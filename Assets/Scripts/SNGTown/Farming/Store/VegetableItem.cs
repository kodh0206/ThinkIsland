using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class VegetableItem : MonoBehaviour
{
    public CropData plant;

    public TMP_Text  nameTxt;
    public TMP_Text priceTxt;
    public Image icon;

    public Button buybutton;

    FarmManager fm;

    // Start is called before the first frame update
    void Start()
    {
        //fm = FindObjectOfType<FarmManager>();
        InitializeUI();
        buybutton.onClick.AddListener(BuyButton);
    }

    public void BuyPlant()
    {
        Debug.Log("Bought " + plant.plantName);
        //fm.SelectPlant(this);
    }

    void InitializeUI()
    {
        nameTxt.text = plant.plantName;
        priceTxt.text = "$" + plant.purchasePrice;
        icon.sprite = plant.growProgressSprites[3];
    }

    void BuyButton()
    {
        Debug.Log(plant.plantName+"구매!"+"수익 : "+plant.sellPrice);
    }
}