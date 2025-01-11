using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject selectedmemberObject, tileObject, tileUnitObject, actionWindowObject, abilityWindowObject, unitSpecifWindowObject, endTurnObject;
    public GameObject abilityListObject;
    [SerializeField] private Text SeriesText, TypesText, AttributesText, HPText, SHDText, NRGText, MNAText,ATKText, MAGText, AMRText, MARText;
    public GameObject SelectedDescription;
    void Awake(){
        Instance = this;
    }

    public void ShowTileInfo(Tile tile){
        if (tile == null){
            tileObject.SetActive(false);
            tileUnitObject.SetActive(false);
            return;
        }

        tileObject.GetComponentInChildren<Text>().text = tile.name;
        tileObject.SetActive(true);

        if (tile.OccupiedUnit){
            tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            tileUnitObject.SetActive(true);
        }
    }

    public void ShowSelectedMember(BaseMember member){
        if (member == null){
            HideSelectedMemberObject();
            return;
        }

        endTurnObject.SetActive(false);

        selectedmemberObject.GetComponentInChildren<Text>().text = member.UnitName;
        selectedmemberObject.SetActive(true);

        SeriesText.text = member.Series;
        TypesText.text = member.ToString("Types");
        AttributesText.text = member.ToString("Attributes");

        HPText.text = member.ToString("Health");
        SHDText.text = member.ToString("Shield");
        NRGText.text = member.ToString("Energy");
        MNAText.text = member.ToString("Mana");
        
        ATKText.text = $"{member.ATK}";
        MAGText.text = $"{member.MAG}";
        AMRText.text = $"{member.AMR}";
        MARText.text = $"{member.MAR}";
    }

    public void UnitSpecifcWindow(bool active){
        unitSpecifWindowObject.SetActive(active);
    }

    public void SetAbilityWindow(bool active){
        abilityWindowObject.SetActive(active);
    }

    public void HideSelectedMemberObject(){
        selectedmemberObject.SetActive(false);
        endTurnObject.SetActive(true);
        SetAbilityWindow(false);
    }
}
