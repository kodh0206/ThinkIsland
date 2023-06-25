using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoreManager : MonoBehaviour
{
    public GameObject plantItem;
    List<CropData> plantObjects = new List<CropData>();
    public Button clsoeButton;
    public RectTransform content;
    public GameObject storePanel;
    private void Awake()
    {   clsoeButton.onClick.AddListener(CloseButton);
        //Assets/Resources/
        var loadPlants = Resources.LoadAll("CropData", typeof(CropData));
        foreach (var plant in loadPlants)
        {
            plantObjects.Add((CropData)plant);
        }
        plantObjects.Sort(SortByPrice);

        foreach (var plant in plantObjects)
        {
            VegetableItem newPlant = Instantiate(plantItem, content).GetComponent<VegetableItem>();
            newPlant.plant = plant;
        }
    }

    int SortByPrice(CropData plantObject1, CropData plantObject2)
    {
        return plantObject1.purchasePrice.CompareTo(plantObject2.purchasePrice);
    }

    int SortByTime(CropData plantObject1, CropData plantObject2)
    {
        return plantObject1.TimesToGrow.CompareTo(plantObject2.TimesToGrow);
    }

    void CloseButton()
    {   Debug.Log("닫아라 상점!");
        storePanel.SetActive(false);
    }
}