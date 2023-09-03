using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggUpgrade : MonoBehaviour
{   
    [SerializeField] EggManager eggManager;
    public int currentLevel = 0;  // 현재 레벨
    public int maxLevel = 4;  // 최대 레벨
    public int[] upgradeCosts = new int[4] { 50, 150, 400, 1000 };  // 각 레벨의 업그레이드 비용
    public int[] upgradeEffect = new int[4] { 5, 15, 30, 50 };  // 각 레벨의 업그레이드 효과
    public Button[] upgradeButtons;  // UI 버튼 배열

    // 팝업창 관련
    public GameObject popup;

    private void Start()
    {
        LoadUpgradeLevel();  // 게임 시작할 때 저장된 업그레이드 레벨 불러오기
        UpdateButtonStates();
    }

    // 업그레이드 버튼을 클릭했을 때
    public void UpgradeLevel(int level)
    {
        if (level == currentLevel + 1 && GameController.Instance.curentgold >= upgradeCosts[level - 1])
        {
            GameController.Instance.curentgold -= upgradeCosts[level - 1];  // 골드 차감
            currentLevel = level;  // 레벨 증가
            eggManager.percent = upgradeEffect[level - 1];  // EggManager 클래스의 업그레이드 함수 호출
            AudioManager.Instance.Playpurchased();
            UpdateButtonStates();  // 버튼 상태 갱신
            SaveUpgradeLevel();  // 업그레이드 레벨 저장
        }
        else
        {
            // 팝업창 활성화
            popup.SetActive(true);
        }
    }

    // 버튼 활성/비활성 상태 갱신
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

    // 골드를 추가하는 함수 (예시)
    public void AddGold(int amount)
    {
        GameController.Instance.curentgold += amount;
    }

    // 업그레이드 레벨 저장
    private void SaveUpgradeLevel()
    {
        ES3.Save<int>("EggUpgradeLevel", currentLevel);
    }

    // 저장된 업그레이드 레벨 불러오기
    private void LoadUpgradeLevel()
    {
        if (ES3.KeyExists("EggUpgradeLevel"))
        {
            currentLevel = ES3.Load<int>("EggUpgradeLevel");
        }
    }
}