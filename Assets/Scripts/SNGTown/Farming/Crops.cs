using System;
using UnityEngine;
[SerializeField]

public enum CropState
{
    Seed,  // 빈 상태 (작물이 심어지지 않음)
    Sprout,  // 작물이 심어진 상태
    Growing,  // 작물이 자라는 상태
    Harvestable  // 작물 수확 가능한 상태
}

public class Crops{
    public string type;
    public int growthTime;
    public int seedPrice;
    public int harvestPrice;

    public Sprite GrowthImage;

    public Crops(string type, int growthTime, int seedPrice, int harvestPrice)
    {
        this.type = type;
        this.growthTime = growthTime;
        this.seedPrice = seedPrice;
        this.harvestPrice = harvestPrice;
    }
}









