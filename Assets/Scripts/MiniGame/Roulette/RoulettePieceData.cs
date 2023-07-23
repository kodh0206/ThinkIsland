using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class RoulettePieceData
{	
	public enum RewardType { Gold, Crop,MiniGame }
    public Sprite icon;              // 보상의 아이콘
    public string description;       // 보상의 설명
    public string rewardType;     
    public int rewardAmount;         // 보상의 양

    [Range(1, 100)]
    public int chance = 100;         // 얻을 확률

    [HideInInspector]
    public int index;                // 보상의 인덱스
    [HideInInspector]
    public int weight;    
}

