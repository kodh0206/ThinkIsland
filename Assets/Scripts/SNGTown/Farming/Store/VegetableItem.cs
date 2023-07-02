using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VegetableItem : MonoBehaviour
{
    public CropData plant;

    public TMP_Text priceTxt;
    public Image icon;
    public Image selectionIndicator; // New UI element to show selection

    public Button buybutton;

    public FarmManager fm;

    // Start is called before the first frame update
    void Start()
    {
        fm = FindObjectOfType<FarmManager>();
        InitializeUI();
        buybutton.onClick.AddListener(BuyButton);
    }


    void InitializeUI()
    {
        
        priceTxt.text = "$" + plant.purchasePrice;
        icon.sprite = plant.storeIcons;
        //selectionIndicator.enabled = false; // Start with selection indicator off
    }

  void BuyButton()
{
    if(fm.money >= plant.purchasePrice) // if the player has enough money
    {   fm.SelectPlant(plant);
        fm.PlantSelectedCrop();// Plant the vegetable
        Debug.Log(plant.plantName+"구매!"+"수익 : "+plant.sellPrice);
        
        //selectionIndicator.enabled = true;
    }
    else
    {
        Debug.Log("Not enough money to buy this plant");
    }
}
}