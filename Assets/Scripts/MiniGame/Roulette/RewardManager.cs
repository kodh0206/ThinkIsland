using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    private static RewardManager _instance;

    public List<LevelRewardData> levelRewards;//레밸마다 열리는 거 정리
    public List<LevelRewardData> newRewards = new List<LevelRewardData>();
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
    public void GetRewardForLevel(int level)
    {
       LevelRewardData reward = levelRewards.FirstOrDefault(r => r.level == level);

    if (reward != null && !RewardAlreadyExists(reward))
    {
        newRewards.Add(reward);
    }

    
    }

    public bool HasNewRewards()
    {
        return newRewards.Count > 0;
    }

    public List<LevelRewardData> GetNewRewards()
    { 
    
    return new List<LevelRewardData>(newRewards);
    }

    public List<RoulettePieceData> ConvertLevelRewardsToPieces(List<LevelRewardData> levelRewards)
    {
        List<RoulettePieceData> convertedRewards = new List<RoulettePieceData>();

    foreach (LevelRewardData reward in levelRewards)
    {
        RoulettePieceData newPiece = new RoulettePieceData();
        if (!reward.unlockedCrop.Equals("None"))
        {
            newPiece.description = reward.unlockedCrop;
            newPiece.icon = reward.CropIcon;
            newPiece.rewardType = RoulettePieceData.RewardType.Crop.ToString();
        }
        else if (!reward.unlockedMiniGame.Equals("None"))
        {
            newPiece.description = reward.unlockedMiniGame;
            newPiece.icon = reward.MiniGameIcon;
            newPiece.rewardType = "MiniGame";
        }

        newPiece.chance = 70;

        // Check if this reward piece already exists
        if (!PieceAlreadyExists(convertedRewards, newPiece))
        {
            convertedRewards.Add(newPiece);
        }
    }

    return convertedRewards;
}
    


public void RemoveFromNewRewards(LevelRewardData rewardToRemove)
{   Debug.Log("new rewards 제거중");
    newRewards.Remove(rewardToRemove);
}
public LevelRewardData GetMatchedNewReward(string description)
{
    return newRewards.FirstOrDefault(r => r.unlockedCrop == description || r.unlockedMiniGame == description);
}

public void RemoveNewReward(LevelRewardData reward)
{
    if (reward != null)
    { 
        newRewards.Remove(reward);
    }
}

private bool RewardAlreadyExists(LevelRewardData reward)
{
    foreach (LevelRewardData existingReward in newRewards)
    {
        if (reward.unlockedCrop.Equals(existingReward.unlockedCrop) || 
            reward.unlockedMiniGame.Equals(existingReward.unlockedMiniGame))
        {
            return true;
        }
    }

    return false;
}

private bool PieceAlreadyExists(List<RoulettePieceData> existingPieces, RoulettePieceData newPiece)
{
    foreach (RoulettePieceData piece in existingPieces)
    {
        if (piece.description.Equals(newPiece.description) &&
            piece.rewardType.Equals(newPiece.rewardType))
        {
            return true;
        }
    }

    return false;
}


}