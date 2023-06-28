using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crops : MonoBehaviour
{
    private CropData curCrop;
    private float timePlanted;
    public SpriteRenderer sr;
    public static event UnityAction<CropData> onPlantCrop;
    public static event UnityAction<CropData> onHarvestCrop;

    public void Plant (CropData crop)
    {
        curCrop = crop;
        timePlanted = Time.time;
        UpdateCropSprite();
        onPlantCrop?.Invoke(crop);
    }

    void UpdateCropSprite ()
    {
        float cropProg = CropProgress();
        int spriteIndex = Mathf.FloorToInt((cropProg / curCrop.TimesToGrow) * curCrop.growProgressSprites.Length);
        spriteIndex = Mathf.Min(spriteIndex, curCrop.growProgressSprites.Length - 1);
        
        if(cropProg < curCrop.TimesToGrow)
        {
            sr.sprite = curCrop.growProgressSprites[spriteIndex];
        }
        else
        {
            //sr.sprite = curCrop.readyToHarvestSprite;
        }
    }

    public void Harvest ()
    {
        if(CanHarvest())
        {
            onHarvestCrop?.Invoke(curCrop);
            Destroy(gameObject);
        }
    }

    float CropProgress ()
    {
        return Time.time - timePlanted;
    }

    public bool CanHarvest ()
    {
        return CropProgress() >= curCrop.TimesToGrow;
    }
}








