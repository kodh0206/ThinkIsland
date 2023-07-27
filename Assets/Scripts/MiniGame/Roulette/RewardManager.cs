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
    {   Debug.Log("현재 래밸 보상획득"+level);
       LevelRewardData reward = levelRewards.FirstOrDefault(r => r.level == level);

    if (reward != null && !RewardAlreadyExists(reward))
    {
        newRewards.Add(reward);
    }
    Debug.Log("보상획득!"+newRewards);
    
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
        if (!reward.unlockedCrop.Equals("None"))
        {
            RoulettePieceData newPiece = new RoulettePieceData();
            newPiece.description = reward.unlockedCrop;
            newPiece.icon = reward.CropIcon;
            newPiece.rewardType = RoulettePieceData.RewardType.Crop.ToString();
            newPiece.chance = 70;

            if (!PieceAlreadyExists(convertedRewards, newPiece))
            {
                convertedRewards.Add(newPiece);
            }
        }
        else if (!reward.unlockedMiniGame.Equals("None"))
        {
            RoulettePieceData newPiece = new RoulettePieceData();
            newPiece.description = reward.unlockedMiniGame;
            newPiece.icon = reward.MiniGameIcon;
            newPiece.rewardType = "MiniGame";
            newPiece.chance = 70;

            if (!PieceAlreadyExists(convertedRewards, newPiece))
            {
                convertedRewards.Add(newPiece);
            }
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
        bool sameCrop = reward.unlockedCrop != null && reward.unlockedCrop.Equals(existingReward.unlockedCrop);
        bool sameMinigame = reward.unlockedMiniGame != null && reward.unlockedMiniGame.Equals(existingReward.unlockedMiniGame);

        // None은 중복이어도 상관없으므로 체크에서 제외합니다.
        if ((sameCrop && reward.unlockedCrop != "None") || 
            (sameMinigame && reward.unlockedMiniGame != "None"))
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
        if ((newPiece.description != "None" && piece.description.Equals(newPiece.description)) &&
            (newPiece.rewardType != "None" && piece.rewardType.Equals(newPiece.rewardType)))
        {
            return true;
        }
    }

    return false;
}


}