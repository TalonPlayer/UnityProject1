using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptableUnits> members, enemies;

    public BaseMember SelectedMember;
    public Tile SelectedTile;
    void Awake(){
        Instance = this;

        members = Resources.LoadAll<ScriptableUnits>("Units").Where(u=>u.Faction == Faction.Member).ToList();

        enemies =Resources.LoadAll<ScriptableUnits>("Units").Where(u=>u.Faction == Faction.Enemy).ToList();
    }

    public void SpawnHeroes(){
        var heroCount = 2;

        for (int i = 0; i < heroCount; i++){
            var spawnedHero = Instantiate(members[i].UnitPrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile(4, 5 - ( 2 * i));

            randomSpawnTile.SetUnitPosition(spawnedHero);
        }

        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies(){
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++){
            var spawnedEnemy = Instantiate(enemies[i].UnitPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            randomSpawnTile.SetUnitPosition(spawnedEnemy);
        }
        GameManager.Instance.ChangeState(GameState.PartyTurn);
    }

    /*
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        // In order,
        // We want all the units with this faction and put it in a list. The order is then randomized. Then the first unit prefab is selected.
        return (T) units.Where(u=>u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }
    */
    public void SetSelectedMember(BaseMember member){
        SelectedMember = member;
        MenuManager.Instance.ShowSelectedMember(member);
        if (member != null){
            CameraManager.Instance.SetCameraToMember(member);
            ButtonManager.Instance.ShowActionButtons();
        }
    }
}
