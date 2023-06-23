using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{   private string mbrId;
    public string MbrId
    {
        get { return mbrId; }
        set { mbrId = value; }
    }

    private string prgsCd;
    public string PrgsCd
    {
        get { return prgsCd; }
        set { prgsCd = value; }
    }

    private int jellyCount;
    public int JellyCount
    {
        get { return jellyCount; }
        set { jellyCount = value; }
    }

    private int goldCount;
    public int GoldCount
    {
        get { return goldCount; }
        set { goldCount = value; }
    }

    private int actionPoints;   
    public int ActionPoints
    {
        get { return actionPoints; }
        set { actionPoints = value; }
    }

    private List<string> unlockedCharacters;
    public List<string> UnlockedCharacters
    {
        get { return unlockedCharacters; }
        set { unlockedCharacters = value; }
}

private List<int> houseLevel;
public List<int> HouseLevel
{
    get { return houseLevel; }
    set { houseLevel = value; }
}

private List<Crops> unlockedCrops;
public List<Crops> UnlockedCrops
{
    get { return unlockedCrops; }
    set { unlockedCrops = value; }
}

private List<LiveStocks> unlockedLivestock;
public List<LiveStocks> UnlockedLivestock
{
    get { return unlockedLivestock; }
    set { unlockedLivestock = value; }
}

private List<Field> ownedFields;
public List<Field> OwnedFields
{
    get { return ownedFields; }
    set { ownedFields = value; }
}

private List<Pastures> ownedPastures;
public List<Pastures> OwnedPastures
{
    get { return ownedPastures; }
    set { ownedPastures = value; }
}

private List<string> upgradedElements;
public List<string> UpgradedElements
{
    get { return upgradedElements; }
    set { upgradedElements = value; }
}

private List<string> unlockedMiniGames;
public List<string> UnlockedMiniGames
{
    get { return unlockedMiniGames; }
    set { unlockedMiniGames = value; }
}

    public void SaveGameData()
    {
        // Create a new dictionary to store the game data
        Dictionary<string, object> saveData = new Dictionary<string, object>();

        // Add the features to the save data
        saveData["mbtId"] =mbrId;
        saveData["prgsCd"] =prgsCd;
        saveData["jellyCount"] = jellyCount;
        saveData["goldCount"] = goldCount;
        saveData["actionPoints"] = actionPoints;
        saveData["unlockedCharacters"] = unlockedCharacters;
        saveData["houseLevel"] = houseLevel;
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
            unlockedCharacters = GetValue<List<string>>(saveData, "unlockedCharacters");
            houseLevel = GetValue<List<int>>(saveData, "houseLevel");
            unlockedCrops = GetValue<List<Crops>>(saveData, "unlockedCrops");
            unlockedLivestock = GetValue<List<LiveStocks>>(saveData, "unlockedLivestock");
            ownedFields = GetValue<List<Field>>(saveData, "ownedFields");
            ownedPastures = GetValue<List<Pastures>>(saveData, "ownedPastures");
            upgradedElements = GetValue<List<string>>(saveData, "upgradedElements");
            unlockedMiniGames = GetValue<List<string>>(saveData, "unlockedMiniGames");
        }
        else
        {
            // No saved data found, initialize with default values
            jellyCount = 0;
            goldCount = 0;
            actionPoints = 0;
            unlockedCharacters = new List<string>();
            houseLevel = new List<int>();
            unlockedCrops = new List<Crops>();
            unlockedLivestock = new List<LiveStocks>();
            ownedFields = new List<Field>();
            ownedPastures = new List<Pastures>();
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