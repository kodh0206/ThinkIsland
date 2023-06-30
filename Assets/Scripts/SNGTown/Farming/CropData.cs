using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Crop Data", menuName = "New Crop Data")]
public class CropData : ScriptableObject
{   public string plantName;
    public float TimesToGrow;
    public Sprite[] growProgressSprites;
    public int purchasePrice;
    public int sellPrice;
    public Sprite storeIcons;
    public Sprite pressedStoreIcon;
    public int unlocklevel;

}