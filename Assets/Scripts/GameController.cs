using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{   
    [SerializeField]
    GameSaveManager gameSaveManager;
    [SerializeField]
    LevelManager levelManager;
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }
    public string currentmbrId;
    public string currentprgsCd;
    public int currentjellyCount;
    public int curentgold;
    public int currentActionPoints;
    public List<string> currentUnlockedAbilities;
    public List<string> currentUnlockedBuildings;
    public List<string> currentUnlockedCrops;
    public List<string> unlockedLivestock;
    public List<string> ownedFields;
    public List<string> ownedPastures;
    public List<string> upgradedElements;
    public List<string> unlockedMiniGames;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
    }
        
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadData(){
        
    }
  
}
