using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiningSystem : MonoBehaviour
{   private static MiningSystem _instance;
    public static MiningSystem Instance { get { return _instance; } }
    public int jellyAccumulated;
    int jellyLimit =18;
    private int jellyMiningTime = 1;
    private int jellyMiningAmount = 1;
    private int miningMachineLevel=0;
    public int[] jellyLimitPerLevel={18,20,23,27,31};

    public TextMeshProUGUI time;
    public TextMeshProUGUI ajelly;

    private DateTime lastSessionTime;

    private void Start()
    {
        LoadGameData();

        // Calculate the offline time
        TimeSpan offlineTime = DateTime.Now - lastSessionTime;

        AutoCollectJellyOffline((float)offlineTime.TotalSeconds);
    }

    private void Update()
    {   
        Debug.Log("젤리 수집시간 "+ jellyMiningTime+"분");
        TimeSpan elapsedTime = DateTime.Now - lastSessionTime;

        if (elapsedTime.TotalSeconds >= jellyMiningTime * 60)
        {
            AccumulateJelly(1);
            lastSessionTime = DateTime.Now;
        }

        UpdateTimerText();
    }

    private void OnDestroy()
    {
        SaveGameData();
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // 게임이 백그라운드로 들어갈 때 실행
            SaveGameData();
        }
        else
        {
            // 게임이 포그라운드로 돌아올 때 실행
            LoadGameData();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadGameData();
    }

    private void AccumulateJelly(int amount)
    {   
        Debug.Log("젤리 수집");
        Debug.Log("체광된 젤리"+amount);
        jellyAccumulated += amount;
        ajelly.text = jellyAccumulated.ToString();
        Debug.Log("누적된 젤리"+ jellyAccumulated);

        if (jellyAccumulated >= jellyLimit)
        {
            CancelJellyMining();
        }
    }

    private void CancelJellyMining()
    {
        jellyAccumulated = 0;
        ajelly.text = "0";
    }

    private void AutoCollectJellyOffline(float offlineTime)
    {
        int offlineJellyAmount = Mathf.FloorToInt(offlineTime / (jellyMiningTime * 60)) * 1;
        jellyAccumulated += offlineJellyAmount;

        if (jellyAccumulated >= jellyLimit)
        {
            CancelJellyMining();
        }
    }

    public void UpgradeMiningMachine()
    {
        miningMachineLevel++;
        jellyLimit = jellyLimitPerLevel[miningMachineLevel];
    }

    private void SaveGameData()
    {
        PlayerPrefs.SetInt("JellyAccumulated", jellyAccumulated);
        PlayerPrefs.SetInt("MiningMachineLevel", miningMachineLevel);
        PlayerPrefs.SetString("LastSessionTime", lastSessionTime.ToBinary().ToString());
        PlayerPrefs.Save();
    }

    private void LoadGameData()
    {
        jellyAccumulated = PlayerPrefs.GetInt("JellyAccumulated", 0);
        miningMachineLevel = PlayerPrefs.GetInt("MiningMachineLevel", 0);
        long tempTime;
        if (long.TryParse(PlayerPrefs.GetString("LastSessionTime", string.Empty), out tempTime))
        {
            lastSessionTime = DateTime.FromBinary(tempTime);
        }
        else
        {
            lastSessionTime = DateTime.Now;
        }

        AutoCollectJellyOffline((float)(DateTime.Now - lastSessionTime).TotalSeconds);
    }

    private void UpdateTimerText()
    {
        TimeSpan remainingTime = TimeSpan.FromMinutes(jellyMiningTime) - (DateTime.Now - lastSessionTime);
        int minutes = remainingTime.Minutes;
        int seconds = remainingTime.Seconds;

        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void CollectJelly()
    {
        GameController.Instance.currentjellyCount += jellyAccumulated;
        CancelJellyMining();
    }
}