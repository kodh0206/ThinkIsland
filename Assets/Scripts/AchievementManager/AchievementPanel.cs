using UnityEngine;
using System.Collections.Generic;

public class AchievementPanel : MonoBehaviour
{
    public GameObject achievementPrefab;
    public Transform contentPanel;

    private List<GameObject> achievementUIObjects = new List<GameObject>();

    private void Start()
    {
        // 초기 로드시 AchievementManager에서 모든 업적을 로드하여 표시 -> 업적 달성 진행 중일 경우에만 로드하여 표시
        LoadAchievements();
    }

    public void LoadAchievements()
    {
        ClearAchievements();

        foreach (Achievement achievement in AchievementManager.Instance.achievements)
        {
            // 업적 달성 진행 중일 경우
            if (achievement.currentAmount > 0)
            {
                GameObject newAchievementUI = Instantiate(achievementPrefab, contentPanel);
                AchievementDefinition achievementDefinition = newAchievementUI.GetComponent<AchievementDefinition>();

                if (achievementDefinition)
                {
                    achievementDefinition.SetAchievement(achievement);
                }

                achievementUIObjects.Add(newAchievementUI);

                // 프로필창 업데이트 (업적 달성 진행도)
                GameController.Instance.achievementProgress += 1;
            }
        }
    }

    public void ClearAchievements()
    {
        foreach (GameObject achievementUI in achievementUIObjects)
        {
            Destroy(achievementUI);
        }
        achievementUIObjects.Clear();
    }
}