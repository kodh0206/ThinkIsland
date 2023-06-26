using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public CropData selectPlant = null; // The selected plant
    public Field selectPlot = null; // The selected plot
    public bool isPlanting = false;
    public int money=100;
    public Text moneyTxt;

    public Color buyColor = Color.green;
    public Color cancelColor = Color.red;

    public bool isSelecting = false;
    public int selectedTool=0;
    // 1- water 2- Fertilizer 3- Buy plot

    public Image[] buttonsImg;
    public Sprite normalButton;
    public Sprite selectedButton;
    public StoreManager storeManager;

    
    // Start is called before the first frame update
    void Start()
    {
        moneyTxt.text = "$" + money;
    }

    private void Update()
    {
        Debug.Log("선택된 작물"+selectPlant.name);
        Debug.Log("선택된 기술"+selectPlot.name);
    }

    public void SelectPlant(CropData newPlant)
    {
        if(selectPlant == newPlant)
        {
            CheckSelection();
            
        }
        else
        {
            CheckSelection();
            selectPlant = newPlant;
            isPlanting = true;
        }
    }

    public void SelectTool(int toolNumber)
    {
        if(toolNumber == selectedTool)
        {
            //deselect
            CheckSelection();
        }
        else
        {
            //select tool number and check to see if anything was also selected
            CheckSelection();
            isSelecting = true;
            selectedTool = toolNumber;
            buttonsImg[toolNumber - 1].sprite = selectedButton;
        }
    }

    void CheckSelection()
    {
        if (isPlanting)
        {
            isPlanting = false;
            if (selectPlant != null)
            {   /*
                selectPlant.btnImage.color = buyColor;
                selectPlant.btnTxt.text = "Buy";
                */
                selectPlant = null;
            }
        }
        if (isSelecting)
        {
            if (selectedTool > 0)
            {
                buttonsImg[selectedTool - 1].sprite = normalButton;
            }
            isSelecting = false;
            selectedTool = 0;
        }
    }

    public void Transaction(int value)
    {
        money += value;
        moneyTxt.text = "$" + money;
    }

     public void SelectPlot(Field plot)
    {
        selectPlot = plot;
        storeManager.OpenStore();
    }
public void PlantSelectedCrop()
{
    // Ensure a crop and a plot have been selected
    if (selectPlant != null && selectPlot != null)
    {
        // Check that the player has enough money
        if (money >= selectPlant.purchasePrice)
        {
            // Subtract the cost from the player's money
            Transaction(-selectPlant.purchasePrice);

            // Plant the crop in the selected plot
            selectPlot.Plant(selectPlant);

            // Deselect the crop and plot
            selectPlant = null;
            selectPlot = null;

            storeManager.storePanel.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough money to plant this crop");
        }
    }
}

public void UnlockPlot(Field plot, int unlockCost)
{
    if (money >= unlockCost)
    {
        Transaction(-unlockCost); // Pay for unlocking the plot
        plot.Unlock();
    }
    else
    {
        Debug.Log("Not enough money to unlock this plot");
    }
}

}