using System;
using UnityEngine;
[SerializeField]
public class Crops
{
    public string name;
    public int growthTime;
    public int seedPrice;
    public int harvestPrice;

    public Crops(string name, int growthTime, int seedPrice, int harvestPrice)
    {
        this.name = name;
        this.growthTime = growthTime;
        this.seedPrice = seedPrice;
        this.harvestPrice = harvestPrice;
    }
}









