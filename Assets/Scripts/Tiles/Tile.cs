using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public string TileName;
    public Vector2 TilePosition;
    [SerializeField] protected SpriteRenderer render;
    [SerializeField] private GameObject highlight;
    [SerializeField] private bool isWalkable;
    [SerializeField] protected Color inRangeColor, baseColor;

    public GameObject Selection;

    public bool InRange;
    public BaseUnit OccupiedUnit;
    public bool Walkable => isWalkable && OccupiedUnit == null;

    public virtual void Init(int x, int y){
        TilePosition = new Vector2(x,y);
    }

    void Update(){
        

    }

    void OnMouseEnter(){
        // If the the menu is open, return; so that highlights aren't enabled.
        if (ButtonManager.Instance.selectedAction == ActionStates.menu) return;
        
        highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
        
    }

    void OnMouseExit(){
        highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

    void OnMouseDown(){

        // If the Game State is not the party's turn, return.
        if(GameManager.Instance.GameState != GameState.PartyTurn) return;

        // If the Menu is opened, tiles can not be clicked on.
        if (ButtonManager.Instance.selectedAction == ActionStates.menu) return;
        

        // The tile contains a unit.
        if (OccupiedUnit != null){
            // The unit on this tile is a memeber, so select that character and run other methods
            if (OccupiedUnit.Faction == Faction.Member) {
                UnitManager.Instance.SetSelectedMember((BaseMember) OccupiedUnit);
                MenuManager.Instance.ShowTileInfo(null);
                UnitManager.Instance.SelectedTile = this;
                Selection.SetActive(false);
            }

            // If the occupied unit is an enemy.
            else if (OccupiedUnit.Faction == Faction.Enemy){
                // If there is a selected memeber, and the character's action is to attack, attack the enemy.
                if (UnitManager.Instance.SelectedMember != null && 
                ButtonManager.Instance.selectedAction == ActionStates.attack && 
                InRange){
                    UnitManager.Instance.SelectedTile.Selection.SetActive(false);
                    var enemy = (BaseEnemy) OccupiedUnit;
                    UnitManager.Instance.SelectedMember.SingleTarget = enemy;
                    Selection.SetActive(true);
                    UnitManager.Instance.SelectedTile = this;
                    UnitManager.Instance.SelectedMember.BaseAttack();

                    CameraManager.Instance.ResetCamera();
                    //UnitManager.Instance.SetSelectedMember(null);
                    GridManager.Instance.HideInRange();
                }
            }

            // Unit can't attack at this spot
            else{
                Debug.Log("Not a valid position!");
            }
        }
        else{

            // If the tile clicked and the selected character's action is to move, move that character to that spot if they are in range.
            if (UnitManager.Instance.SelectedMember != null && 
            ButtonManager.Instance.selectedAction == ActionStates.move && 
            isWalkable && InRange){                    
                
                Debug.Log(UnitManager.Instance.SelectedTile.name);
                UnitManager.Instance.SelectedTile.Selection.SetActive(false);
                Selection.SetActive(true);
                UnitManager.Instance.SelectedTile = this;
                

                UnitManager.Instance.SelectedMember.Move(this);
                CameraManager.Instance.ResetCamera();
                // UnitManager.Instance.SetSelectedMember(null);
            }
            else{
                Debug.Log("Not a valid position!");
            }
        }
    }

    public void SetUnitPosition(BaseUnit unit){
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
        unit.Position = TilePosition;
        GridManager.Instance.HideInRange();
    }

    public virtual void SetRangeBool(bool active){
        InRange = active;
        render.color = active ? inRangeColor: baseColor;
    }
}
