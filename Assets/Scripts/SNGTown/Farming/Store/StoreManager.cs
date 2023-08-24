using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoreManager : MonoBehaviour
{
    public GameObject plantItem;
    public BetaManager farmManager;
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
            // Check if the player owns this crop
            if (GameController.Instance.currentUnlockedCrops.Contains((CropData)plant))
            {
                plantObjects.Add((CropData)plant);
            }
        }

        foreach (var plant in plantObjects)
        {
            VegetableItem newPlant = Instantiate(plantItem, content).GetComponent<VegetableItem>();
            newPlant.plant = plant;
            newPlant.fm = farmManager;
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

    public void OpenStore()
    {   UpdateStore();
        storePanel.SetActive(true);
    }

    void CloseButton()
    {   Debug.Log("닫아라 상점!");
        storePanel.SetActive(false);
    }

    public void UpdateStore()
{
    // Clear the current store items
    foreach (Transform child in content)
    {
        Destroy(child.gameObject);
    }

    // Add the updated items
    plantObjects.Clear();
    foreach (var plant in Resources.LoadAll("CropData", typeof(CropData)))
    {
        if (GameController.Instance.currentUnlockedCrops.Contains((CropData)plant))
        {
            plantObjects.Add((CropData)plant);
            VegetableItem newPlant = Instantiate(plantItem, content).GetComponent<VegetableItem>();
            newPlant.plant = (CropData)plant;
            newPlant.fm = farmManager;
        }
    }
}
}