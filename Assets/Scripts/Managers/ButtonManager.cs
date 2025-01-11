using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;
    [SerializeField] private GameObject attackButton, abilityButton, moveButton, blockButton;
    public AbilityButtons AbilityButtonPrefab;
    private Button aButton;
    public ActionStates selectedAction;

    void Awake(){
        Instance = this;
        selectedAction = ActionStates.none;
        aButton = abilityButton.GetComponentInChildren<Button>();
    }
    public void ShowActionButtons(){
        selectedAction = ActionStates.menu;
        attackButton.SetActive(true);
        abilityButton.SetActive(true);
        moveButton.SetActive(true);
        blockButton.SetActive(true);
    }
    public void HideActionButtons(){
        attackButton.SetActive(false);
        abilityButton.SetActive(false);
        moveButton.SetActive(false);
        blockButton.SetActive(false);
        aButton.enabled = true;
    }

    public void Move(){
        selectedAction = ActionStates.move;
        MenuManager.Instance.HideSelectedMemberObject();
        CameraManager.Instance.ResetCamera();
        GridManager.Instance.TilesInRange(UnitManager.Instance.SelectedMember.MoveRange, UnitManager.Instance.SelectedMember.OccupiedTile.name);
        HideActionButtons();
    }

    public void Attack(){
        selectedAction = ActionStates.attack;
        MenuManager.Instance.HideSelectedMemberObject();
        CameraManager.Instance.ResetCamera();
        GridManager.Instance.TilesInRange(UnitManager.Instance.SelectedMember.AttackRange, UnitManager.Instance.SelectedMember.OccupiedTile.name);
        HideActionButtons();
    }

    public void AbilityList(){
        UnitManager.Instance.SelectedMember.ShowAbilityButtons();
        MenuManager.Instance.SetAbilityWindow(true);
        aButton.enabled = false;
    }
    public void Block(){
        
    }

    public void Exit(){
        selectedAction = ActionStates.none;
        if (aButton) UnitManager.Instance.SelectedMember.HideAbilityButtons();
        MenuManager.Instance.HideSelectedMemberObject();
        CameraManager.Instance.ResetCamera();
        HideActionButtons();
    }

    public void UnitWindow(bool active){
        MenuManager.Instance.UnitSpecifcWindow(active);
    }

    public void EndTurn(){
        GameManager.Instance.GameState = GameState.RunActions;
        TurnManager.Instance.RunActions();
    }
}

public enum ActionStates{
    attack,
    move,
    block,
    menu,
    none
}
