using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    [Header("Achievement List")]
    public List<Achievement> achievements = new List<Achievement>();

    private void Awake()
    {if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Don't destroy this object when a new scene is loaded.
        DontDestroyOnLoad(gameObject);
    }

    // 해당 ID를 가진 업적을 반환합니다.
    public Achievement GetAchievementByID(string id)
    {
        return achievements.Find(achievement => achievement.id == id);
    }

    // 업적의 현재 진행 상태를 증가시킵니다.
    public void IncrementAchievement(string id, int amount)
    {
        Achievement achievement = GetAchievementByID(id);
        if (achievement != null)
        {
            achievement.currentAmount += amount;

            // RequiredAmount를 초과하지 않도록 조정
            achievement.currentAmount = Mathf.Clamp(achievement.currentAmount, 0, achievement.requiredAmount);

            // 필요한 경우 다른 처리를 여기에 추가하십시오. (예: 업적 완료 알림 등)
        }
        else
        {
            Debug.LogError("Achievement with ID: " + id + " not found.");
        }
    }
}