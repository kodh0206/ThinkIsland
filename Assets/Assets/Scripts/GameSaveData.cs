using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
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
    public CharacterSaveData characterData;

    public GameSaveData()
    {
        unlockedAbilities = new List<string>();
        unlockedBuildings = new List<string>();
        unlockedCrops = new List<string>();
        unlockedLivestock = new List<string>();
        ownedFields = new List<string>();
        ownedPastures = new List<string>();
        upgradedElements = new List<string>();
        unlockedMiniGames = new List<string>();
        characterData = new CharacterSaveData();
    }
}
