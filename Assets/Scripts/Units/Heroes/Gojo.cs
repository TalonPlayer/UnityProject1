using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gojo : BaseMember
{
    public override void CreateAbilityButtons(Image abilityImage, string abilityName, int index)
    {
        string targetType = "", cost = "", description = "";
        switch (abilityName){
            case "Test Ability 1":
                targetType = "Test 1";
                cost = "Test 1";
                description = "Test 1";
                break;
            case "Test Ability 2":
                targetType = "Test 2";
                cost = "Test 2";
                description = "Test 2";
                break;
            default:
                break;
        }
        abilities[index].SetButtonDetails(abilityImage, abilityName, targetType, cost, description);
    }
}
