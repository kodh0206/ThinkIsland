using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager _instance;
    public static MiniGameManager Instance { get { return _instance; } }

    public Canvas minigameUI;
    public CanvasGroup fadeCanvasGroup; // 페이드 효과를 위한 CanvasGroup
    public bool minigameUIActive;
    [SerializeField] private List<string> miniGameScenes = new List<string>();
    private string currentMiniGameScene;
    [SerializeField]private List<string> remainingMiniGameScenes = new List<string>();
    //public TextMeshProUGUI remaingames;
    private float gameChangeInterval = 10f; // 미니게임 변경 간격
    private float timer = 0f;
    public int totalJelly; //먹은 젤리의 갯수 

    private int gamesToPlay=5;
    private bool isMiniGameScene = false; // 현재 씬이 미니게임 씬인지 여부를 확인하기 위한 플래그
    private float deltaTime = 0.0f;

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
    remainingMiniGameScenes.Clear();

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
    }

   

    private void Update()
    {
        if (isMiniGameScene)
        {
            timer += Time.deltaTime;
            if (timer >= gameChangeInterval)
            {
                timer = 0f;
                StartNextMiniGame();
            }
        }
        Debug.Log(gamesToPlay);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BetaScene")
    {
        if(minigameUI != null) 
        {
            minigameUI.gameObject.SetActive(false);
        }

        isMiniGameScene = false; // 미니게임 씬이 아님을 표시
    }
    else
    {
        if(minigameUI != null) 
        {
            minigameUI.gameObject.SetActive(true);
        }

        isMiniGameScene = true; // 미니게임 씬임을 표시
    }
    }

    public void LoadMainMenu()
    {  
        Debug.Log("로딩완료");
        SceneManager.LoadScene("BetaScene");

        isMiniGameScene = false; // 미니게임 씬이 아님을 표시

        // Initialize the remaining games
        remainingMiniGameScenes.Clear();

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

        if(minigameUI != null) 
        {
            minigameUI.gameObject.SetActive(false);
        }
    }

    public void StartMiniGame()
    {
        if (remainingMiniGameScenes.Count == 0)
        {   
            // 모든 미니게임을 클리어한 경우
            LoadMainMenu();
            return;
        }

        if(minigameUI != null) 
        {
            minigameUI.gameObject.SetActive(true);
        }

        StartCoroutine(FadeAndLoadScene());
    }

    public void StartNextMiniGame()
    {
        if (remainingMiniGameScenes.Count == 0)
        {   
            Debug.Log("total jellies"+totalJelly);
            totalJelly =0;
            SaveTotalJelly();
            LoadMainMenu();
            Debug.Log("얻은 젤리 수"+totalJelly);
            return;
        }
        //remaingames.text = remainingMiniGameScenes.Count.ToString();
        StartCoroutine(FadeAndLoadScene());
    }

    private IEnumerator FadeAndLoadScene()
    {
        yield return StartCoroutine(Fade(1f));

        string nextMiniGameScene = remainingMiniGameScenes[0];
        remainingMiniGameScenes.RemoveAt(0);
        SceneManager.LoadScene(nextMiniGameScene, LoadSceneMode.Single);//미니게임 로드
        currentMiniGameScene = nextMiniGameScene;

        isMiniGameScene = true; // 미니게임 씬임을 표시

        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float finalAlpha)
    {
        float fadeSpeed = Mathf.Abs(fadeCanvasGroup.alpha - finalAlpha) / 1f;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, finalAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);

            yield return null;
        }
    }

    public void AddJelly()
    {
        totalJelly += 1;
        GameController.Instance.currentjellyCount+=1;
        minigameUI.GetComponent<MIniGameUI>().UpdateJellyText();  // UI 업데이트
    }

    public void MiniGameFinished()
    {   
        StartNextMiniGame();
    }

    public void SaveTotalJelly()
    {
        GameController.Instance.currentjellyCount +=totalJelly;
        Debug.Log("얻은 젤리 수"+totalJelly);
    }

    public void LoadTotalJelly()
    {
        totalJelly = PlayerPrefs.GetInt("totalJelly", 0);
    }
    

    private void OnApplicationQuit()
    {
        miniGameScenes.Clear();
    }
}