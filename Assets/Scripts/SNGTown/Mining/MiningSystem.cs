using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MiningSystem : MonoBehaviour
{
  public int jellyAccumulated; // 누적된 젤리 개수
    public int jellyLimit; // 젤리 누적 한도

    public int jellyMiningTime=10; // 젤리 적재 시간 간격(분)
    public int jellyMiningAmount; // 젤리 적재 개수

    public int miningMachineLevel; // 광맥 강화기 레벨
    public int[] jellyLimitPerLevel; // 광맥 강화기 레벨별 젤리 한도 배열

    private float elapsedTime; // 경과 시간
    private float totalElapsedTime; // 전체 경과 시간

    private float lastSessionTime; // 마지막으로 게임을 종료한 시간
    private float offlineAccumulatedTime; // 접속하지 않은 동안의 누적 시간
    
    public TextMeshProUGUI time;
    public TextMeshProUGUI ajelly;
    private void Start()
    {   
        LoadGameData();

    // 다른 초기화 작업들...
        lastSessionTime = Time.realtimeSinceStartup;

         
        // 게임 시작 시간 기록
       
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        totalElapsedTime += Time.deltaTime;

    // 일정 시간마다 젤리 추가 적재
    if (elapsedTime >= jellyMiningTime)
    {
        elapsedTime = 0f;
        AccumulateJelly(jellyMiningAmount);
    }

    // 일정 시간이 지나면 자동 채집
    if (totalElapsedTime >= jellyMiningTime * 10) // 예시로 10분마다 채집
    {
        totalElapsedTime = 0f;
        AutoCollectJelly();
    }
    Debug.Log(jellyMiningTime +"체집 시간"+ elapsedTime+"경과시간"+ "미접속 시간"+totalElapsedTime);
    UpdateTimerText();
    }

    private void OnDestroy()
    {   
        lastSessionTime = Time.realtimeSinceStartup;
        // 게임 종료 시 데이터 저장
        SaveGameData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // 게임이 백그라운드로 들어갈 때 실행
            lastSessionTime = Time.realtimeSinceStartup;
        }
        else
        {
            // 게임이 포그라운드로 돌아올 때 실행
            float offlineTime = Time.realtimeSinceStartup - lastSessionTime;
            offlineAccumulatedTime += offlineTime;

            // 젤리 자동 채집
            AutoCollectJellyOffline(offlineTime);
        }
    }
private void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
    SceneManager.sceneUnloaded += OnSceneUnloaded;
}

private void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
    SceneManager.sceneUnloaded -= OnSceneUnloaded;
}
private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    // 씬이 로드되었을 때 게임 데이터 로드
    LoadGameData();
}

private void OnSceneUnloaded(Scene scene)
{
    // 씬이 언로드될 때 게임 데이터 저장
    SaveGameData();
}

private void AccumulateJelly(int amount)
    {
        // 젤리 누적 및 한도 체크
        jellyAccumulated += amount;
        ajelly.text =jellyAccumulated.ToString();
       if (jellyAccumulated >= jellyLimit)
        {
        CancelJellyMining();
        }
        else
        {
        elapsedTime = 0f; // elapsedTime 초기화
        }
    }

    private void CancelJellyMining()
    {   
        // 젤리 적재 취소
        jellyAccumulated = 0;
        ajelly.text = "0";
        elapsedTime = 0f; // elapsedTime 초기화
    }

    private void AutoCollectJelly()
    {
        // 자동 젤리 채집
        jellyAccumulated += jellyLimit;
        if (jellyAccumulated >= jellyLimit)
        {
            CancelJellyMining();
        }
    }

    private void AutoCollectJellyOffline(float offlineTime)
    {
        // 자동 젤리 채집 (접속하지 않은 동안)
        int offlineJellyAmount = Mathf.FloorToInt(offlineTime / (jellyMiningTime * 60)) * jellyMiningAmount;
        jellyAccumulated += offlineJellyAmount;
        if (jellyAccumulated >= jellyLimit)
        {
            CancelJellyMining();
        }
    }

    public void UpgradeMiningMachine()
    {
        // 광맥 강화기 업그레이드
        miningMachineLevel++;
        jellyLimit = jellyLimitPerLevel[miningMachineLevel];
    }

    private void SaveGameData()
    {
        PlayerPrefs.SetInt("JellyAccumulated", jellyAccumulated);
        PlayerPrefs.SetInt("MiningMachineLevel", miningMachineLevel);
        PlayerPrefs.SetFloat("LastSessionTime", lastSessionTime);
        PlayerPrefs.SetFloat("OfflineAccumulatedTime", offlineAccumulatedTime);
        PlayerPrefs.SetFloat("ElapsedTime", elapsedTime); // 이 줄 추가
        PlayerPrefs.SetFloat("TotalElapsedTime", totalElapsedTime); // 이 줄 추가
        PlayerPrefs.Save();
        
    }

    private void LoadGameData()
    {
        jellyAccumulated = PlayerPrefs.GetInt("JellyAccumulated", 0);
        miningMachineLevel = PlayerPrefs.GetInt("MiningMachineLevel", 0);
        lastSessionTime = PlayerPrefs.GetFloat("LastSessionTime", 0f);
        offlineAccumulatedTime = PlayerPrefs.GetFloat("OfflineAccumulatedTime", 0f);
        elapsedTime = PlayerPrefs.GetFloat("ElapsedTime", elapsedTime); // 이 줄 수정
        totalElapsedTime = PlayerPrefs.GetFloat("TotalElapsedTime", totalElapsedTime); // 이 줄 수정

        float offlineTime = Time.realtimeSinceStartup - lastSessionTime;
        AutoCollectJellyOffline(offlineTime);
    }

    private void UpdateTimerText()
    {
        // 남은 체광 시간 계산
        float remainingTime = (jellyMiningTime * 60) - (elapsedTime + offlineAccumulatedTime);
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        
        // 텍스트 업데이트
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        Debug.Log("남은시간"+remainingTime+"시간!");
    }

    public void CollectJelly()
    {
        GameController.Instance.currentjellyCount += jellyAccumulated;
    }
}
