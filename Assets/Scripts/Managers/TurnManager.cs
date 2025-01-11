using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public List<Action> Actions = new List<Action>();

    void Awake(){
        Instance = this;
    }

    public void RunActions(){
        foreach(Action action in Actions){
            action.Invoke();
        }

        GameManager.Instance.GameState = GameState.PartyTurn;
    }
}
