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
    
    //해금될 농작물리스트 
    public List<CropData> CropList = new List<CropData>();
    public int currentjellyCount;
    public int curentgold;
    public int currentActionPoints;
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
    }
        
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadData(){
        
    }


       public void GainExperience(int amount)
    {
        current_experience += amount;

        // 현재 레벨의 필요 경험치를 초과하면 레벨업을 처리
        while (current_experience >= expToLevelUp[level-1])
        {
            LevelUp();
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

    
}
