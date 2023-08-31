using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class EggManager : MonoBehaviour
{   
    
    public int fixedGoldAmount = 100;
    public bool isHarvestable = false;
    public DateTime lastHarvestTime;

    // UI Components
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI timerText;
    public Button harvestButton;

    public int percent=0;




    private void Start()
    {
        LoadData();
        harvestButton.interactable = isHarvestable;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        TimeSpan elapsed = DateTime.Now - lastHarvestTime;

        if (elapsed.TotalMinutes >= 10)
        {
            isHarvestable = true;
            harvestButton.interactable = true;
        }

        UpdateTimerText();
    }

    public void OnHarvestButtonClicked()
    {
    if (isHarvestable)
    {
        int currentGold = fixedGoldAmount;

        // percent가 0이 아닐 때만 적용
        if (percent != 0)
        {
            currentGold += (int)(fixedGoldAmount * (1+(percent * 0.01f)));
        }
        else
        {
            currentGold += fixedGoldAmount;
        }
        isHarvestable = false;
        harvestButton.interactable = false;
        lastHarvestTime = DateTime.Now;
        SaveData();
        goldText.text = "Gold: " + currentGold;
    }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("IsHarvestable", isHarvestable ? 1 : 0);
        PlayerPrefs.SetString("LastHarvestTime", lastHarvestTime.ToBinary().ToString());
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        isHarvestable = PlayerPrefs.GetInt("IsHarvestable", 0) == 1;

        long tempTime;
        if (long.TryParse(PlayerPrefs.GetString("LastHarvestTime", string.Empty), out tempTime))
        {
            lastHarvestTime = DateTime.FromBinary(tempTime);
        }
        else
        {
            lastHarvestTime = DateTime.Now;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadData();
    }

    private void UpdateTimerText()
    {
        if (isHarvestable)
        {
            timerText.text = "Harvest!";
        }
        else
        {
            TimeSpan remaining = TimeSpan.FromMinutes(10) - (DateTime.Now - lastHarvestTime);
            timerText.text = string.Format("{0:D2}:{1:D2}", remaining.Minutes, remaining.Seconds);
        }
    }
}