using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMember : BaseUnit
{
    public abstract void CreateAbilityButtons(Image abilityImage, string abilityName, int index);

    public void HideAbilityButtons(){
        foreach(AbilityButtons button in abilities){
            Destroy(button.gameObject);
        }
    }
    public void ShowAbilityButtons()
    {
        abilities = new List<AbilityButtons>();
        for (int i = 0; i < abilityName.Count; i++){
            abilities.Add(Instantiate(ButtonManager.Instance.AbilityButtonPrefab, MenuManager.Instance.abilityListObject.transform));
            CreateAbilityButtons(abilityImage[i], abilityName[i], i);
        }
    }
}
