using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager _instance;
    public static MiniGameManager Instance { get { return _instance; } }

    [SerializeField] private List<string> miniGameScenes = new List<string>();
    private string currentMiniGameScene;
    private List<string> remainingMiniGameScenes = new List<string>();

    private float gameChangeInterval = 10f; // 미니게임 변경 간격
    private float timer = 0f;

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
    }

    private void Start()
    {
        LoadMainMenu();
    }

    private void Update()
    {
        // 미니게임 변경 시간 간격 체크
        if (currentMiniGameScene != null && !string.IsNullOrEmpty(currentMiniGameScene))
        {
            timer += Time.deltaTime;
            if (timer >= gameChangeInterval)
            {
                timer = 0f;
                StartMiniGame();
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BetaScene")
        {
            // Beta Scene이 로드되었을 때 remainingMiniGameScenes 초기화
            remainingMiniGameScenes.Clear();
            remainingMiniGameScenes.AddRange(miniGameScenes);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("BetaScene");
    }

    public void StartMiniGame()
    {
        if (remainingMiniGameScenes.Count == 0)
        {
            // 모든 미니게임을 클리어한 경우
            LoadMainMenu();
            return;
        }

        string nextMiniGameScene = remainingMiniGameScenes[0];
        remainingMiniGameScenes.RemoveAt(0);

        SceneManager.LoadScene(nextMiniGameScene, LoadSceneMode.Single);
        currentMiniGameScene = nextMiniGameScene;
    }

    public void MiniGameFinished()
    {
        StartMiniGame();
    }
}