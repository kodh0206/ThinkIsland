using UnityEngine;
using System.Collections.Generic;

public class AchievementPanel : MonoBehaviour
{
    public GameObject achievementPrefab;
    public Transform contentPanel;

    private List<GameObject> achievementUIObjects = new List<GameObject>();

    private void Start()
    {
        // 초기 로드시 AchievementManager에서 모든 업적을 로드하여 표시
        LoadAchievements();
    }

    public void LoadAchievements()
    {
        ClearAchievements();

        foreach (Achievement achievement in AchievementManager.Instance.achievements)
        {
            GameObject newAchievementUI = Instantiate(achievementPrefab, contentPanel);
            AchievementDefinition achievementDefinition = newAchievementUI.GetComponent<AchievementDefinition>();

            if (achievementDefinition)
            {
                achievementDefinition.SetAchievement(achievement);
            }

            achievementUIObjects.Add(newAchievementUI);
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