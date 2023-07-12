using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MiningSystem : MonoBehaviour
{
  public int jellyAccumulated; // 누적된 젤리 개수
    public int jellyLimit; // 젤리 누적 한도

    public int jellyMiningTime; // 젤리 적재 시간 간격(분)
    public int jellyMiningAmount; // 젤리 적재 개수

    public int miningMachineLevel; // 광맥 강화기 레벨
    public int[] jellyLimitPerLevel; // 광맥 강화기 레벨별 젤리 한도 배열

    private float elapsedTime; // 경과 시간
    private float totalElapsedTime; // 전체 경과 시간

    private float lastSessionTime; // 마지막으로 게임을 종료한 시간
    private float offlineAccumulatedTime; // 접속하지 않은 동안의 누적 시간

    public TextMeshProUGUI time;
    private void Start()
    {
        // 저장된 데이터 로드
        LoadGameData();

        // 경과 시간 초기화
        elapsedTime = 0f;
        totalElapsedTime = 0f;

        // 게임 시작 시간 기록
        lastSessionTime = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        // 경과 시간 업데이트
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
    }

    private void OnDestroy()
    {
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

    private void AccumulateJelly(int amount)
    {
        // 젤리 누적 및 한도 체크
        jellyAccumulated += amount;

        if (jellyAccumulated >= jellyLimit)
        {
            CancelJellyMining();
        }
    }

    private void CancelJellyMining()
    {
        // 젤리 적재 취소
        jellyAccumulated = 0;
        elapsedTime = 0f;
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
        // 추후 E3Save 사용 예정
        PlayerPrefs.SetInt("JellyAccumulated", jellyAccumulated);
        PlayerPrefs.SetInt("MiningMachineLevel", miningMachineLevel);
        PlayerPrefs.SetFloat("LastSessionTime", lastSessionTime);
        PlayerPrefs.SetFloat("OfflineAccumulatedTime", offlineAccumulatedTime);
        PlayerPrefs.Save();
    }

    private void LoadGameData()
    {
        // 저장된 데이터 불러오기
        jellyAccumulated = PlayerPrefs.GetInt("JellyAccumulated", 0);
        miningMachineLevel = PlayerPrefs.GetInt("MiningMachineLevel", 0);
        lastSessionTime = PlayerPrefs.GetFloat("LastSessionTime", 0f);
        offlineAccumulatedTime = PlayerPrefs.GetFloat("OfflineAccumulatedTime", 0f);

        // 누적된 시간에 따른 자동 젤리 채집 (접속하지 않은 동안)
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
        time.text = string.Format("Remaining Time: {0:00}:{1:00}", minutes, seconds);
    }
}
