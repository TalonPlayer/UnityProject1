using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtons : MonoBehaviour
{
    [SerializeField] private GameObject descriptionBox;
    [SerializeField] private TextMeshProUGUI abilityName, targetType, cost, description;
    [SerializeField] private Image abilityImage;
    public void ActivateDescription(){
        descriptionBox.SetActive(true);
        if (MenuManager.Instance.SelectedDescription != null) MenuManager.Instance.SelectedDescription.SetActive(false);
        
        MenuManager.Instance.SelectedDescription = descriptionBox;
    }

    public void SetButtonDetails(Image abilityImage, string abilityName, string targetType, string cost, string description){
        this.abilityImage = abilityImage;
        this.abilityName.text = abilityName;
        this.targetType.text = targetType;
        this.cost.text = cost;
        this.description.text = description;
    }
}
