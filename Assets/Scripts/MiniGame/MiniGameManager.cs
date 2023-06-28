using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager _instance;
    public static MiniGameManager Instance { get { return _instance; } }

    public Canvas minigameUI;
    public bool minigameUIActive;
    [SerializeField] private List<string> miniGameScenes = new List<string>();
    private string currentMiniGameScene;
    private List<string> remainingMiniGameScenes = new List<string>();

    private float gameChangeInterval = 10f; // 미니게임 변경 간격
    private float timer = 0f;
    public int totalJelly; //먹은 젤리의 갯수 

    private bool isMiniGameScene = false; // 현재 씬이 미니게임 씬인지 여부를 확인하기 위한 플래그

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
        
        //LoadTotalJelly();
    }

    private void Start()
    {
        LoadMainMenu();
        
    }

    private void Update()
    {
        // 미니게임 변경 시간 간격 체크
        if (isMiniGameScene)
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
    if (scene.name == "MiniGameTrain")
    {
        // Beta Scene이 로드되었을 때 remainingMiniGameScenes 초기화
        remainingMiniGameScenes.Clear();
        remainingMiniGameScenes.AddRange(miniGameScenes);

        // minigameUI.gameObject.SetActive(false); 이 코드를 삭제하고 아래 코드를 추가합니다.
        minigameUI.enabled = false;

        isMiniGameScene = false; // 미니게임 씬이 아님을 표시
    }
    else
    {
        isMiniGameScene = true; // 미니게임 씬임을 표시
        // minigameUI.gameObject.SetActive(true); 이 코드를 삭제하고 아래 코드를 추가합니다.
        minigameUI.enabled = true;
    }

    }

    public void LoadMainMenu()
    {  
        Debug.Log("로딩완료");
        SceneManager.LoadScene("MiniGameTrain");
    }
    public void StartMiniGame()
    {
        // 첫 미니게임 시작
    if (remainingMiniGameScenes.Count == 0)
    {
        // 모든 미니게임을 클리어한 경우
        LoadMainMenu();
        //SaveTotalJelly();
        return;
    }
    minigameUI.gameObject.SetActive(true);
    string nextMiniGameScene = remainingMiniGameScenes[0];
    remainingMiniGameScenes.RemoveAt(0);

    SceneManager.LoadScene(nextMiniGameScene, LoadSceneMode.Single);//미니게임 로드
    currentMiniGameScene = nextMiniGameScene;

    Debug.Log("첫 미니게임 시작: " + currentMiniGameScene);
    }

public void StartNextMiniGame()
{
    // 다음 미니게임 시작
    if (remainingMiniGameScenes.Count == 0)
    {    Debug.Log("total jellies"+totalJelly);
        totalJelly =0;
        // 모든 미니게임을 클리어한 경우
       
        LoadMainMenu();
        Debug.Log("얻은 젤리 수"+totalJelly);
        //SaveTotalJelly();
        return;
    }

    string nextMiniGameScene = remainingMiniGameScenes[0];
    remainingMiniGameScenes.RemoveAt(0);

    SceneManager.LoadScene(nextMiniGameScene, LoadSceneMode.Single);//미니게임 전환 
    currentMiniGameScene = nextMiniGameScene;

    Debug.Log("다음 미니게임 시작: " + currentMiniGameScene);
}
public void AddJelly()
{
    totalJelly += 1;
    minigameUI.GetComponent<MIniGameUI>().UpdateJellyText();  // UI 업데이트
      // 변경된 젤리 수 저장
}
public void MiniGameFinished()
{  
    StartNextMiniGame();
}
    public void SaveTotalJelly()
    {
        //PlayerPrefs.SetInt("totalJelly", totalJelly);
        Debug.Log("얻은 젤리 수"+totalJelly);
    }

    public void LoadTotalJelly()
    {
        totalJelly = PlayerPrefs.GetInt("totalJelly", 0);
    }
}