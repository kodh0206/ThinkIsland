using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementButtonClick : MonoBehaviour
{

    public GameObject sngCanvas;
    public MonoBehaviour mobileTouchScript;

    int activatedCharacterCount = 0; // 활성화된 characterObjects의 수를 저장할 변수

    void Start()
    {
        AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
        if (achievementManager != null)
        {
            int[] requiredCharacterCounts = { 4, 8, 12, 16, 20 };
            string[] achievementIds = { "28", "29", "30", "31", "32" };

            for (int i = 0; i < requiredCharacterCounts.Length; i++)
            {
                if (GameController.Instance.unlockedMiniGames.Count >= requiredCharacterCounts[i])
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

    void Update()
    {
        
    }

    public void AchievementCloseButtonClick()
    {
        gameObject.SetActive(false);
        sngCanvas.SetActive(true);
        mobileTouchScript.enabled = true;
    }
}
