using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LiveStock Data", menuName = "New LiveStock Data")]
public class LiveStockData : MonoBehaviour
{
    public string LiveStockName;
    public float TimesToProduce;
    //public Sprite[] growProgressSprites;
    public int purchasePrice;
    public int sellPrice;
    public Sprite storeIcons;
    public Sprite pressedStoreIcon;
    public int unlocklevel;
}
