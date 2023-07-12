using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningSystem : MonoBehaviour
{public int jellyAccumulated; // 누적된 젤리 개수
    public int jellyLimit; // 젤리 누적 한도

    public int jellyMiningTime; // 젤리 적재 시간 간격(분)
    public int jellyMiningAmount; // 젤리 적재 개수

    public int miningMachineLevel; // 광맥 강화기 레벨
    public int[] jellyLimitPerLevel ={18,20,23,27,31}; // 광맥 강화기 레벨별 젤리 한도 배열

    private float elapsedTime; // 경과 시간

    private void Start()
    {
        // 초기화
        jellyAccumulated = 0;
        jellyLimit = jellyLimitPerLevel[miningMachineLevel];
        elapsedTime = 0f;
    }

    private void Update()
    {
        // 경과 시간 업데이트
        elapsedTime += Time.deltaTime;

        // 일정 시간마다 젤리 추가 적재
        if (elapsedTime >= jellyMiningTime)
        {
            elapsedTime = 0f;
            AccumulateJelly(jellyMiningAmount);
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

    public void UpgradeMiningMachine()
    {
        // 광맥 강화기 업그레이드
        miningMachineLevel++;
        jellyLimit = jellyLimitPerLevel[miningMachineLevel];
    }
}
