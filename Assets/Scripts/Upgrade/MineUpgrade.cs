using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MineUpgrade : MonoBehaviour
{   [SerializeField] MiningSystem miningSystem;
    public int currentLevel = 0;  // 현재 레밸
    public int maxLevel = 4;  // 최대 레밸
    public int[] upgradeCosts = new int[4] { 50, 150, 400, 1000 };  // 각 레밸의 업그레이드 비용
    public Button[] upgradeButtons;  // UI 버튼들
    
    // 팝업창 관련
    public GameObject popup;

    private void Start()
    {           
        LoadUpgradeLevel();  // 게임 시작 시 저장된 업그레이드 레벨을 불러옵니다.
        UpdateButtonStates();
    }

    // 업그레이드 버튼을 눌렀을 때
    public void UpgradeLevel(int level)
    {
        // 레밸 검사
        if (level == currentLevel + 1 && GameController.Instance.curentgold >= upgradeCosts[level - 1])
        {
            GameController.Instance.curentgold -= upgradeCosts[level - 1];  // 골드 소모
            currentLevel = level;  // 레밸 증가
            miningSystem.UpgradeMiningMachine();
            AudioManager.Instance.Playpurchased();
            UpdateButtonStates();  // 버튼 상태 갱신
            SaveUpgradeLevel();  // 업그레이드 후 레벨을 저장합니다.
        }
        else
        {
            // 팝업창 활성화
            // popup.SetActive(true);
        }

    }

    // 버튼 활성화/비활성화 상태를 갱신
    private void UpdateButtonStates()
    {
        for (int i = 0; i < maxLevel; i++)
        {
            if (i == currentLevel)
                upgradeButtons[i].interactable = true;
            else
                upgradeButtons[i].interactable = false;
        }
    }

    // 다른 곳에서 골드를 추가하는 함수 (예시)
    public void AddGold(int amount)
    {
        GameController.Instance.curentgold+= amount;
    }

        private void SaveUpgradeLevel()
    {
        ES3.Save<int>("MineUpgradeLevel", currentLevel);
    }

    // 저장된 업그레이드 레벨을 불러오는 함수
    private void LoadUpgradeLevel()
    {
        if (ES3.KeyExists("MineUpgradeLevel"))
        {
            currentLevel = ES3.Load<int>("MineUpgradeLevel");
        }
    }
}
