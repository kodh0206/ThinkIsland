using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedCharacterCollection : MonoBehaviour
{

    // Mg1Character부터 Mg20Character까지 순서대로 저장
    public GameObject[] characterObjects;
    int i = 1;

    int activatedCharacterCount = 0; // 활성화된 characterObjects의 수를 저장할 변수
    
    void Start()
    {

        for (int i = 0; i < characterObjects.Length; i++)
        {
            string characterName = "Mg" + (i + 1);
            if (GameController.Instance.unlockedMiniGames.Contains(characterName))
            {
                characterObjects[i].SetActive(true);
                activatedCharacterCount++; // 활성화된 characterObjects의 수 증가
            }
        }

        AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
        if (achievementManager != null)
        {
            int[] requiredCharacterCounts = { 4, 8, 12, 16, 20 };
            string[] achievementIds = { "28", "29", "30", "31", "32" };

            for (int i = 0; i < requiredCharacterCounts.Length; i++)
            {
                if (activatedCharacterCount >= requiredCharacterCounts[i])
                {
                    achievementManager.IncrementAchievement(achievementIds[i], requiredCharacterCounts[i]);
                }
                else
                {
                    break;
                }
            }
        }

    }
}