using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUnit : MonoBehaviour
{
    // Info
    public string UnitName, TransformationName;
    public string UnitID;
    public string Series;
    public List<string> Attributes, Types;


    // Ability Info
    [SerializeField] protected List<string> abilityName;
    [SerializeField] protected List<Image> abilityImage;

    protected List<AbilityButtons> abilities = new List<AbilityButtons>();
    // Stats

    // Health, Attack, Magic, Armor, Magic Resist, Energy, Mana
    [SerializeField] public float HP, SHD, ATK, MAG, AMR, MAR, NRG, MNA;
    [SerializeField] public bool isAlive, isDisabled, isTransformer, isSummoner;
    public int AttackRange, MoveRange;

    // Default Values
    private float defHP, defATK, defMAG, defAMR, defMAR, defNRG, defMNA;
    // Max Values
    private float maxHP, maxNRG, maxMNA;

    // Manager Variables
    public Tile OccupiedTile;
    public Faction Faction;   
    public string SelectedAction;
    public BaseUnit SingleTarget;
    public List<BaseUnit> TargetedUnits;
    public Facing Facing;
    public Vector2 Position;

    void Awake(){
        maxHP = HP;
        maxNRG = NRG;
        maxMNA = MNA;

        defHP = HP;
        defATK = ATK;
        defMAG = MAG;
        defAMR = AMR;
        defMAR = MAR;
        defNRG = NRG;
        defMNA = MNA;
    }
    public void Teleport(Tile tile){
        tile.SetUnitPosition(this);
    }

    public void ResetAction(){
        SelectedAction = "";
    }

    public void Move(Tile tile){
        TurnManager.Instance.Actions.Add(() => tile.SetUnitPosition(this));
    }
    public void BaseAttack(){
        TurnManager.Instance.Actions.Add(() => DamageTarget(SingleTarget, ATK));
    }
    public void DamageTarget(BaseUnit unit, float damage){
        unit.TakeDamage(damage);
    }

    public void TakeDamage(float damage){
        HP -= damage;
        CheckHealth();
    }

    public virtual void CheckHealth(){
        if (HP <= 0){
            HP = 0;
            isAlive = false;
            Destroy(gameObject);
        }
    }

    public virtual string ToString(string type){
        switch (type)
        {
            case "Types":
                return ListToString(Types);
            case "Attributes":
                return ListToString(Attributes);
            case "Health":
                return $"{HP} / {maxHP}";
            case "Energy":
                return $"{NRG} / {maxNRG}";
            case "Mana":
                return $"{MNA} / {maxMNA}";
            case "Shield":
                return $"{SHD}";
            default:
                return $"NULL";
        }
    }

    public string ListToString(List<string> list){
        string temp = "";
        for (int i = 0; i < list.Count; i++)
        {
            temp += $"{list[i]}";
            if (i < list.Count - 1){
                temp += ", ";
            }
        }
        return temp;
    }
}

public enum Facing{
    Left,
    Right
}
