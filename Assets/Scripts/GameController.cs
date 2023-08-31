using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{   
    [SerializeField]
    GameSaveManager gameSaveManager;
    [SerializeField]
    
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }
    public string currentmbrId;
    public string currentprgsCd;
    
    //심사위원 제출용 변수 
    public bool isDeveloperMode =false;
    //해금될 농작물리스트 
    public List<CropData> CropList = new List<CropData>();
    public int currentjellyCount;
    public int curentgold;
    public int currentActionPoints; // 에너지 
    public int maximumActionPoints; //최대에너지 
    public int level=1;
    public int[] expToLevelUp = {
       /* 5, 37, 60, 78, 92,104,115,124,133,140, //1~10
        147,154,159,165,170,175,175,179,184,188,//11~20
        192,195,199,202,205,209,212,214,217,220,//21~30
        222,225,227,230,232,234,237,239,241,243,//31~40
        245,247,248,250,252,254,256,257,259,260,//41~50
        262,264,265,267,268,269,271,272,274,275,//51~60
        276,277,279,280,282,282,284,285,286,287,//61~70
        288,289,290,292,293,294,295,296,297,298,//71~80
        299,300,301,302,303,303,304,305,306,307,//81~90
        308,309,310,311,311,312,313,314,315 //90~99
        */
        1,2,3,4,5,6,7,8,9,10,
        11,12,13,14,15,16,17,18,19,
        20,21,22,23,24,25,26,27,28,29,
        30,31,32,33,34,35,36,37,38,39
        ,40,41,42,43,44,45,46,47,48,49
        ,50,51,52,53,54,55,56,57,58,59
        ,50,51,52,53,54,55,56,57,58,59
        ,50,51,52,53,54,55,56,57,58,59
        ,50,51,52,53,54,55,56,57,58,59
        ,50,51,52,53,54,55,56,57,58,59
        }; 
        // 각 레벨별 필요한 경험치
    public int current_experience;
    public int max_experience;
    public List<string> currentUnlockedAbilities;
    public List<string> currentUnlockedBuildings;
    public List<CropData> currentUnlockedCrops;
    public List<string> unlockedLivestock;
    public List<string> ownedFields;
    public List<string> ownedPastures;
    public List<string> upgradedElements;
    public List<string> unlockedMiniGames;
    public string current_language ="Korean";
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

        AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
        if (achievementManager != null)
        {
            achievementManager.IncrementAchievement("33", level);
        }
    }
        
    

    // public void Update()
    // {
    //     AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
    //     if (achievementManager != null)
    //     {
    //         achievementManager.IncrementAchievement("33", level);
    //     }
    // }



       public void GainExperience(int amount)
    {
        current_experience += amount;

        // 현재 레벨의 필요 경험치를 초과하면 레벨업을 처리
        while (current_experience >= expToLevelUp[level-1])
        {
            LevelUp();

            AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
            if (achievementManager != null)
            {
                achievementManager.IncrementAchievement("33", 1);
            }
        }
       
    }

    void LevelUp()
    { 
       // 레벨 업
    level++;
    Debug.Log(level+ "현재 레밸!");

    // 현재 레벨의 필요한 경험치를 초과한 값은 다음 레벨의 경험치로 계속 유지
    current_experience = current_experience - expToLevelUp[level - 2];

    // 새 레벨에 대한 처리를 수행. 예: 능력치 증가, 새로운 능력 잠금 해제 등
    // ...

    // Get the reward data for the new level
     RewardManager.Instance.GetRewardForLevel(level);
   
    
    }

      public void AddGold(int amount)
    {
        curentgold += amount;
    }

        void LoadData(){
        if (ES3.KeyExists("currentmbrId"))
            currentmbrId = ES3.Load<string>("currentmbrId");
        if (ES3.KeyExists("currentprgsCd"))
            currentprgsCd = ES3.Load<string>("currentprgsCd");
        if (ES3.KeyExists("currentjellyCount"))
            currentjellyCount = ES3.Load<int>("currentjellyCount");
        if (ES3.KeyExists("curentgold"))
            curentgold = ES3.Load<int>("curentgold");
        if (ES3.KeyExists("currentActionPoints"))
            currentActionPoints = ES3.Load<int>("currentActionPoints");
        if (ES3.KeyExists("maximumActionPoints"))
            maximumActionPoints = ES3.Load<int>("maximumActionPoints");
        if (ES3.KeyExists("level"))
            level = ES3.Load<int>("level");
        if (ES3.KeyExists("current_experience"))
            current_experience = ES3.Load<int>("current_experience");
        if (ES3.KeyExists("max_experience"))
            max_experience = ES3.Load<int>("max_experience");
        if (ES3.KeyExists("current_language"))
            current_language = ES3.Load<string>("current_language");
        if (ES3.KeyExists("currentUnlockedAbilities"))
            currentUnlockedAbilities = ES3.Load<List<string>>("currentUnlockedAbilities");
        if (ES3.KeyExists("currentUnlockedBuildings"))
            currentUnlockedBuildings = ES3.Load<List<string>>("currentUnlockedBuildings");
        if (ES3.KeyExists("currentUnlockedCrops"))
            currentUnlockedCrops = ES3.Load<List<CropData>>("currentUnlockedCrops");
        if (ES3.KeyExists("unlockedLivestock"))
            unlockedLivestock = ES3.Load<List<string>>("unlockedLivestock");
        if (ES3.KeyExists("ownedFields"))
            ownedFields = ES3.Load<List<string>>("ownedFields");
        if (ES3.KeyExists("ownedPastures"))
            ownedPastures = ES3.Load<List<string>>("ownedPastures");
        if (ES3.KeyExists("upgradedElements"))
            upgradedElements = ES3.Load<List<string>>("upgradedElements");
        if (ES3.KeyExists("unlockedMiniGames"))
            unlockedMiniGames = ES3.Load<List<string>>("unlockedMiniGames");
    }
        
    
    public void SaveData()
    {
        ES3.Save("currentmbrId", currentmbrId);
        ES3.Save("currentprgsCd", currentprgsCd);
        ES3.Save("currentjellyCount", currentjellyCount);
        ES3.Save("curentgold", curentgold);
        ES3.Save("currentActionPoints", currentActionPoints);
        ES3.Save("maximumActionPoints", maximumActionPoints);
        ES3.Save("level", level);
        ES3.Save("current_experience", current_experience);
        ES3.Save("max_experience", max_experience);
        ES3.Save("current_language", current_language);
        ES3.Save("currentUnlockedAbilities", currentUnlockedAbilities);
        ES3.Save("currentUnlockedBuildings", currentUnlockedBuildings);
        ES3.Save("currentUnlockedCrops", currentUnlockedCrops);
        ES3.Save("unlockedLivestock", unlockedLivestock);
        ES3.Save("ownedFields", ownedFields);
        ES3.Save("ownedPastures", ownedPastures);
        ES3.Save("upgradedElements", upgradedElements);
        ES3.Save("unlockedMiniGames", unlockedMiniGames);
    }
}
