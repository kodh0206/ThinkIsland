using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    private static RewardManager _instance;

    public List<LevelRewardData> levelRewards;
    private List<LevelRewardData> newRewards = new List<LevelRewardData>();
    public static RewardManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RewardManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<RewardManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    //레밸에 맞는 보상추가 
    public LevelRewardData GetRewardForLevel(int level)
    {
        LevelRewardData reward = levelRewards[level - 1];

        if (reward != null)
        {
            newRewards.Add(reward);
        }

        return reward;
    }

    public bool HasNewRewards()
    {
        return newRewards.Count > 0;
    }

    public List<LevelRewardData> GetNewRewards()
    {
        List<LevelRewardData> rewardsToReturn = new List<LevelRewardData>(newRewards);
        newRewards.Clear();
        return rewardsToReturn;
    }

    public List<RoulettePieceData> ConvertLevelRewardsToPieces(List<LevelRewardData> levelRewards)
    {
        List<RoulettePieceData> convertedRewards = new List<RoulettePieceData>();

        foreach (LevelRewardData reward in levelRewards)
        {
            RoulettePieceData newPiece = new RoulettePieceData();
            if (reward.unlockedCrop != null)
            {
                newPiece.description = reward.unlockedCrop;
                newPiece.icon = reward.CropIcon;
                newPiece.rewardType = RoulettePieceData.RewardType.Crop.ToString();
            }
            else if (reward.unlockedMiniGame != null)
            {
                newPiece.description = reward.unlockedMiniGame;
                newPiece.icon = reward.MiniGameIcon;
                newPiece.rewardType = "MiniGame";
            }

            newPiece.chance = 1;

            convertedRewards.Add(newPiece);
        }

        return convertedRewards;
    }

    public void AddReward(LevelRewardData newReward)
{
    levelRewards.Add(newReward);
}


}