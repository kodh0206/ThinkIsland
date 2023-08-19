using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdeventureUpgrade : MonoBehaviour
{ public int currentLevel = 0;  // 현재 레밸
    public int maxLevel = 4;  // 최대 레밸
    public int[] upgradeCosts = new int[4] { 100, 200, 300, 400 };  // 각 레밸의 업그레이드 비용
    public int[] upgradEffect = new int[4] {5,10,15,20};
    public Button[] upgradeButtons;  // UI 버튼들
    private void Start()
    {
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
            MiniGameManager.Instance.jellypercentage=upgradEffect[level-1]; //효과변경 
            UpdateButtonStates();  // 버튼 상태 갱신
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
}
