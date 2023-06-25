using System;
using UnityEngine;
public class Field : MonoBehaviour
{
    public enum PlotState {LOCKED, EMPTY, PLANTING};
    public PlotState state = PlotState.EMPTY;
    public Sprite lockedSprite;
    public Sprite emptySprite;
    public CropData currentCropData;
    public SpriteRenderer plotSprite;
    public SpriteRenderer cropSprite;
    private float timeLeft;
    private int stage;

    private void Awake()
    {
        plotSprite = GetComponent<SpriteRenderer>();
        cropSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        ChangeState(state);
    }

    private void Update()
    {
        switch (state)
        {
            case PlotState.EMPTY:
                /*if (GameManager.instance.selectedCropData != null)
                {
                    Plant(GameManager.instance.selectedCropData);
                }
                */
                break;
            case PlotState.PLANTING:
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    stage++;
                    if (stage < currentCropData.growProgressSprites.Length)
                    {
                        timeLeft = currentCropData.TimesToGrow;
                        cropSprite.sprite = currentCropData.growProgressSprites[stage];
                    }
                    else
                    {
                        Harvest();
                    }
                }
                break;
        }
    }

    private void OnMouseDown()
    {
        if (state == PlotState.PLANTING)
        {
            Harvest();
        }
    }

    private void Plant(CropData cropData)
    {
        if (state == PlotState.EMPTY)
        {
            state = PlotState.PLANTING;
            currentCropData = cropData;
            timeLeft = cropData.TimesToGrow;
            stage = 0;
            cropSprite.sprite = cropData.growProgressSprites[stage];
            //GameManager.instance.ChangeMoney(-cropData.buyPrice);
        }
    }

    private void Harvest()
    {
        if (state == PlotState.PLANTING)
        {
            //GameManager.instance.ChangeMoney(currentCropData.sellPrice);
            currentCropData = null;
            cropSprite.sprite = null;
            ChangeState(PlotState.EMPTY);
        }
    }

    private void ChangeState(PlotState newState)
    {
        state = newState;
        switch (state)
        {
            case PlotState.LOCKED:
                plotSprite.sprite = lockedSprite;
                break;
            case PlotState.EMPTY:
                plotSprite.sprite = emptySprite;
                break;
            case PlotState.PLANTING:
                plotSprite.sprite = emptySprite;
                break;
        }
    }
}

    



