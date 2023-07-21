using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
     public static RewardManager Instance { get; private set; }

    public List<LevelRewardData> levelRewards;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public LevelRewardData GetRewardForLevel(int level)
    {
        return levelRewards.FirstOrDefault(reward => reward.level == level);
    }
}