using System;
using UnityEngine;
using UnityEngine.Events;



public class Crops:MonoBehaviour{

    private CropData curCrop;
    private float plantDay;
    public SpriteRenderer sr;
    public static event UnityAction<CropData> onPlantCrop;
    public static event UnityAction<CropData> onHarvestCrop;
    public void Plant (CropData crop)
    {
        curCrop = crop;
        //plantDay = GameManager.instance.curDay;//생성된 시간
        //daysSinceLastWatered = 1;
        UpdateCropSprite();
        onPlantCrop?.Invoke(crop);
    }
    float CropProgress ()
    {   return 0f;
        //return GameManager.instance.curDay - plantDay;
    }
    // Can we currently harvest the crop?
    public bool CanHarvest ()
    {   return false;
        //return CropProgress() >= curCrop.daysToGrow;
    }

    public void Harvest ()
    {
        if(CanHarvest())
        {
            onHarvestCrop?.Invoke(curCrop);
            Destroy(gameObject);
        }
    }


    void UpdateCropSprite ()
    {
        /*
        void UpdateCropSprite ()
        {
            int cropProg = CropProgress();
            if(cropProg < curCrop.daysToGrow)
            {
                sr.sprite = curCrop.growProgressSprites[cropProg];
            }
            else
            {
                sr.sprite = curCrop.readyToHarvestSprite;
            }
    }
        
        
        
        
        */
    }

}









