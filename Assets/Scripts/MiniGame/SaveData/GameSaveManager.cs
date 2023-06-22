using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public int jellyCount;
    public int goldCount;
    public int actionPoints;
    public List<string> unlockedAbilities;
    public List<string> unlockedBuildings;
    public List<string> unlockedCrops;
    public List<string> unlockedLivestock;
    public List<string> ownedFields;
    public List<string> ownedPastures;
    public List<string> upgradedElements;
    public List<string> unlockedMiniGames;

    private void SaveGameData()
    {
        // Create a new dictionary to store the game data
        Dictionary<string, object> saveData = new Dictionary<string, object>();

        // Add the features to the save data
        saveData["jellyCount"] = jellyCount;
        saveData["goldCount"] = goldCount;
        saveData["actionPoints"] = actionPoints;
        saveData["unlockedAbilities"] = unlockedAbilities;
        saveData["unlockedBuildings"] = unlockedBuildings;
        saveData["unlockedCrops"] = unlockedCrops;
        saveData["unlockedLivestock"] = unlockedLivestock;
        saveData["ownedFields"] = ownedFields;
        saveData["ownedPastures"] = ownedPastures;
        saveData["upgradedElements"] = upgradedElements;
        saveData["unlockedMiniGames"] = unlockedMiniGames;

        // Save the data using Easy Save
        ES3.Save<Dictionary<string, object>>("gameSaveData", saveData);
    }

    private void LoadGameData()
    {
        // Load the saved data using Easy Save
        if (ES3.KeyExists("gameSaveData"))
        {
            // Retrieve the saved data
            Dictionary<string, object> saveData = ES3.Load<Dictionary<string, object>>("gameSaveData");

            // Retrieve the features from the save data
            jellyCount = GetValue<int>(saveData, "jellyCount");
            goldCount = GetValue<int>(saveData, "goldCount");
            actionPoints = GetValue<int>(saveData, "actionPoints");
            unlockedAbilities = GetValue<List<string>>(saveData, "unlockedAbilities");
            unlockedBuildings = GetValue<List<string>>(saveData, "unlockedBuildings");
            unlockedCrops = GetValue<List<string>>(saveData, "unlockedCrops");
            unlockedLivestock = GetValue<List<string>>(saveData, "unlockedLivestock");
            ownedFields = GetValue<List<string>>(saveData, "ownedFields");
            ownedPastures = GetValue<List<string>>(saveData, "ownedPastures");
            upgradedElements = GetValue<List<string>>(saveData, "upgradedElements");
            unlockedMiniGames = GetValue<List<string>>(saveData, "unlockedMiniGames");
        }
        else
        {
            // No saved data found, initialize with default values
            jellyCount = 0;
            goldCount = 0;
            actionPoints = 0;
            unlockedAbilities = new List<string>();
            unlockedBuildings = new List<string>();
            unlockedCrops = new List<string>();
            unlockedLivestock = new List<string>();
            ownedFields = new List<string>();
            ownedPastures = new List<string>();
            upgradedElements = new List<string>();
            unlockedMiniGames = new List<string>();
        }
    }

    private T GetValue<T>(Dictionary<string, object> data, string key)
    {
        if (data.ContainsKey(key))
            return (T)data[key];
        
        return default(T);
    }
}