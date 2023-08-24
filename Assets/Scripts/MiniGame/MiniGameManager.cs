using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;
using Unity.VisualScripting;

public class MiniGameManager : MonoBehaviour
{   
    private static MiniGameManager _instance;
    public static MiniGameManager Instance { get { return _instance; } }
    public static event Action OnMiniGameStart;
    public static event Action OnMiniGameEnd;

    public Canvas minigameUI;
    public CanvasGroup fadeCanvasGroup; // 페이드 효과를 위한 CanvasGroup
    public bool minigameUIActive;
    [SerializeField] private List<string> miniGameScenes = new List<string>();
    private string currentMiniGameScene;
    [SerializeField]private List<string> remainingMiniGameScenes = new List<string>();
    //public TextMeshProUGUI remaingames;
    private float gameChangeInterval = 10f; // 미니게임 변경 간격
    private float timer = 0f;
    public int totalJelly=0; //먹은 젤리의 갯수 

    public int jellypercentage =0;
    private int gamesToPlay=5;
    public bool isMiniGameScene = false; // 현재 씬이 미니게임 씬인지 여부를 확인하기 위한 플래그
    private float deltaTime = 0.0f;
    public int gameLevel;
    
    public float countdownStartTime = 3f;
    private bool isCountDown =false;

     public int obstacleHitCount;
    public float obstacleHitTimer;
    public int difficultyLevel;
    public int maxDifficultyLevel = 4;
    public int minDifficultyLevel = 1;
    public int GameScore = 0;
    private void Awake()
    {
    if (_instance != null && _instance != this)
    {
        Destroy(gameObject);
        return;
    }

    _instance = this;
    DontDestroyOnLoad(gameObject);
    SceneManager.sceneLoaded += OnSceneLoaded;

    // miniGameScenes 리스트 초기화
    miniGameScenes.Clear();
    miniGameScenes.AddRange(GameController.Instance.unlockedMiniGames);

    // remainingMiniGameScenes 리스트 초기화
    //remainingMiniGameScenes.Clear();

    // While we don't have selected games to play
    while (remainingMiniGameScenes.Count < gamesToPlay)
    {
        // Pick a random game
        int randomIndex = UnityEngine.Random.Range(0, miniGameScenes.Count);
        
        // If it's not already in our list, add it
        if (!remainingMiniGameScenes.Contains(miniGameScenes[randomIndex]))
        {
            remainingMiniGameScenes.Add(miniGameScenes[randomIndex]);
        }
    }
  
    fadeCanvasGroup.alpha = 0f; // 초기에는 페
    }

    private void Start()
    {
    LoadMainMenu();
    Debug.Log("게임시작");
    fadeCanvasGroup.alpha = 0f;
    GameScore = 0;
    DOTween.KillAll();
    }

      public void StartMiniGameWithAudio()
    {
        AudioManager.Instance.PlayMiniGameAudio();
    }

    private void Update()
    {
        if (isMiniGameScene)
        {
            timer += Time.deltaTime;

            if (timer >= gameChangeInterval - countdownStartTime && !isCountDown)
            {
            isCountDown = true;
            AudioManager.Instance.MiniGameExchange();
            StartCoroutine(Countdown());
            }

            if (timer >= gameChangeInterval)
            {   MIniGameUI.Instance.CountDown.gameObject.SetActive(false);
                timer = 0f;
                StartNextMiniGame();
                isCountDown = false;
            }
        }
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {   
        
        
       if (scene.name == "BetaScene" || scene.name == "Main" || scene.name == "ClassScene" || scene.name == "Roulette")
    {   

        miniGameScenes.Clear();
        miniGameScenes.AddRange(GameController.Instance.unlockedMiniGames);
        if(minigameUI != null) 
        {
            minigameUI.gameObject.SetActive(false);
        }
        isMiniGameScene = false; // 미니게임 씬이 아님을 표시
    }
    else // Main 씬이 아닌 경우에만 랜덤 BGM 재생
    {    DOTween.KillAll();
        if(minigameUI != null) 
        {
            minigameUI.gameObject.SetActive(true);
        }
        AudioManager.Instance.PlayRandomBgm();
        isMiniGameScene = true; // 미니게임 씬임을 표시
    }
    }

  public void LoadMainMenu()
{   
    Debug.Log("로딩완료");

    isMiniGameScene = false; // 미니게임 씬이 아님을 표시

    // Initialize the remaining games
    //remainingMiniGameScenes.Clear();

    // While we don't have selected games to play
    while (remainingMiniGameScenes.Count < gamesToPlay)
    {    
        Debug.Log(remainingMiniGameScenes.Count);
        // Pick a random game
        int randomIndex = UnityEngine.Random.Range(0, miniGameScenes.Count);

        // If it's not already in our list, add it
        if (!remainingMiniGameScenes.Contains(miniGameScenes[randomIndex]))
        {
            remainingMiniGameScenes.Add(miniGameScenes[randomIndex]);
        }
    }

    // Check if we have enough mini games to play
    if(remainingMiniGameScenes.Count < gamesToPlay)
    {
        // If not, show a black screen (or any other error handling you want)
        Debug.Log("Not enough mini games to play. Showing a black screen.");
        StartCoroutine(ShowBlackScreen());
        return;
    }

    if(minigameUI != null) 
    {
        minigameUI.gameObject.SetActive(false);
    }
    
    DOTween.KillAll();
    StartCoroutine(FadeAndLoadScene("BetaScene"));
}

// A coroutine that makes the screen go black by using the fade effect
private IEnumerator ShowBlackScreen()
{
    yield return StartCoroutine(Fade(1f));
}

   public void StartMiniGame()
    {   
        
        if (remainingMiniGameScenes.Count == 0)
        {   
            LoadMainMenu();
            return;
        }

        if(minigameUI != null) 
        {
            minigameUI.gameObject.SetActive(true);
        }
      
        AudioManager.Instance.audioSource.Stop(); // Stop BGM
        StartCoroutine(FadeAndLoadScene());
    }
  public void StartNextMiniGame()
    {
        if (remainingMiniGameScenes.Count == 0)
        {   DOTween.KillAll();
            totalJelly =0;
            SaveTotalJelly();
            LoadRouletteScene();
            return;
        }
        AudioManager.Instance.audioSource.Stop(); // Stop BGM
        StartCoroutine(FadeAndLoadScene());
    }


   private IEnumerator FadeAndLoadScene(string sceneName = null)
{
    string nextMiniGameScene;
        if (sceneName == null)
        {
            nextMiniGameScene = remainingMiniGameScenes[0];
            remainingMiniGameScenes.RemoveAt(0);
        }
        else
        {
            nextMiniGameScene = sceneName;
        }

        SceneManager.LoadScene(nextMiniGameScene, LoadSceneMode.Single);  // 미니게임 로드
        currentMiniGameScene = nextMiniGameScene;

        if (sceneName == null)
        {
            isMiniGameScene = true; // 미니게임 씬임을 표시
        }

        yield return null;
    }


private IEnumerator Fade(float finalAlpha)
    {
    float fadeSpeed = Mathf.Abs(fadeCanvasGroup.alpha - finalAlpha) / 0.3f; // Try 0.3f here

    while (!Mathf.Approximately(fadeCanvasGroup.alpha, finalAlpha))
    {
        fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, finalAlpha,
            fadeSpeed * Time.deltaTime);

        yield return null;
    }
}

    private IEnumerator Countdown()
{   MIniGameUI.Instance.CountDown.gameObject.SetActive(true);
    
    float countdownTime = countdownStartTime;
    while (countdownTime > 0)
    {
        MIniGameUI.Instance.CountDown.text = countdownTime.ToString("F0");
        yield return new WaitForSeconds(1f);
        countdownTime -= 1f;
    }
    MIniGameUI.Instance.CountDown.text = ""; // 카운트 다운이 끝나면 텍스트를 비움
}
    

    public void MiniGameFinished()
    {   
        StartNextMiniGame();
    }

    public void SaveTotalJelly()
    {   if(jellypercentage !=0)
        {
            totalJelly += totalJelly * (jellypercentage / 100);

        }
        GameController.Instance.currentjellyCount +=totalJelly;
        Debug.Log("얻은 젤리 수"+totalJelly);
    }

    public void LoadTotalJelly()
    {
        totalJelly = PlayerPrefs.GetInt("totalJelly", 0);
    }
    
   public void AddJelly()
    {
        totalJelly += 1;
        GameController.Instance.currentjellyCount+=1;
        GameController.Instance.GainExperience(1);
        minigameUI.GetComponent<MIniGameUI>().UpdateJellyText();  // UI 업데이트
        AudioManager.Instance.PlayJelly();
    }
    public void AddMiniGame(string minigame)
    {
        remainingMiniGameScenes.Insert(0,minigame);
    }
    private void LoadRouletteScene()
    {
    StartCoroutine(FadeAndLoadScene("Roulette"));
    }
    private void OnApplicationQuit()
    {
        miniGameScenes.Clear();
    }


    public void IncreaseDifficulty()
    {
        if (difficultyLevel < maxDifficultyLevel)
        {
            difficultyLevel++;
        }
    }

    public void DecreaseDifficulty()
    {
        if (difficultyLevel > minDifficultyLevel)
        {
            difficultyLevel--;
        }
    }

    public void IncreaseScore()
    {
        GameScore++;
    }
    public void ResetScore()
    {
        GameScore=0;
    }
    public int LoadDifficulty()
    {
        return difficultyLevel;
    }
    public int LoadScore()
    {
        return GameScore;
    }



}