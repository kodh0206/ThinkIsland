using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameController : MonoBehaviour
{   

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
        5, 37, 60, 78, 92,104,115,124,133,140, //1~10
        147,154,159,165,170,175,175,179,184,188,//11~20
        192,195,199,202,205,209,212,214,217,220,//21~30
        222,225,227,230,232,234,237,239,241,243,//31~40
        245,247,248,250,252,254,256,257,259,260,//41~50
       
      
        }; 
        // 각 레벨별 필요한 경험치
    public int current_experience;
    public int max_experience;
    public List<CropData> currentUnlockedCrops;
    public List<string> unlockedMiniGames;
    public bool isKorean =true;
    
    
    // 프로필 관련 변수 초기화
    public int jellyCount = 0; // 젤리 획득
    public int goldAmount = 0; // 골드 획득
    public int playMiniGame = 0;// 탐험 입장 (미니게임 플레이 횟수)
    public int noCrashObject = 0;// 장애물 충돌없이 클리어
    public int quizCorrect = 0;// 퀴즈 정답
    public int goldenBell = 0;// 골든벨
    public int mapExtend = 0;// 맵 확장 단계
    public int charCollectionProgress = 0;// 캐릭터 수집 진행도
    public int achievementProgress = 0;// 업적 달성 진행도
    public int playTime = 0;// 게임 플레이타임

    public int hasFarm =0;;

    
    public bool isLiveStockIslandUnlocked =false;
    public bool isDesertIslandUnlocked=false;

    public bool isWinterlandUnlocked =false;

    public bool isTropicLandUnlocked =false;


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
        Application.targetFrameRate = 60;

        AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
        if (achievementManager != null)
        {
            achievementManager.IncrementAchievement("33", level);
        }

        // 프로필창 업데이트 (캐릭터 수집 진행도)
        charCollectionProgress = unlockedMiniGames.Count;

    if (isDeveloperMode)  // 마스터 모드가 활성화되었는지 확인
        {
            UnlockEverything();  // 모든 것을 해금하는 함수 호출
        }
    }
        
    
  
 
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
    if (level == 10) {
        isLiveStockIslandUnlocked = true;
        Debug.Log("목축섬 해금!");
    }
    if (level == 20) {
        isDesertIslandUnlocked = true;
        Debug.Log("사막섬 해금!");
    }
    if (level == 30) {
        isWinterlandUnlocked = true;
        Debug.Log("겨울섬 해금!");
    }
    if (level == 40) {
        isTropicLandUnlocked = true;
        Debug.Log("열대섬 해금!");
    }
    
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
        if (ES3.KeyExists("unlockedMiniGames"))
            unlockedMiniGames = ES3.Load<List<string>>("unlockedMiniGames");
    }
        
    
   public void SaveData()
{   
    if(isDeveloperMode ==false)
    {
    // 기존 코드
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
    ES3.Save("currentUnlockedCrops", currentUnlockedCrops);
    ES3.Save("unlockedMiniGames", unlockedMiniGames);

    // 새로 추가한 섬 해금 상태 저장
    ES3.Save("isLiveStockIslandUnlocked", isLiveStockIslandUnlocked);
    ES3.Save("isDesertIslandUnlocked", isDesertIslandUnlocked);
    ES3.Save("isWinterlandUnlocked", isWinterlandUnlocked);
    ES3.Save("isTropicLandUnlocked", isTropicLandUnlocked);
    }
}

    void UnlockEverything()
{   
    isDeveloperMode = true;
    // 모든 농작물 해금
    currentUnlockedCrops = new List<CropData>(CropList);

    // 모든 미니게임 해금 (미니게임 리스트를 가정)
    unlockedMiniGames = new List<string> { /* 모든 미니게임 리스트 */ };

    // 모든 섬 해금
    isLiveStockIslandUnlocked = true;
    isDesertIslandUnlocked = true;
    isWinterlandUnlocked = true;
    isTropicLandUnlocked = true;

    // 필요한 경우 여기에 추가 코드를 작성
}



      private void OnApplicationQuit()
    {
        SaveData();
    }

    public void Reset()
    {   
        ES3.DeleteFile();
        PlayerPrefs.DeleteAll();
    }
}
