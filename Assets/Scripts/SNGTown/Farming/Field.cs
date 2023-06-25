using System;
using UnityEngine;
/*
농작물 심는 방법
1. 타일 클릭시 상점 UI 생성
2. 상점 UI에서 농작물 선택
3. 그러면 농작물 선택 되고
4.그리고 심기 


*/


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

    public GameObject vegetablePanel;
    private void Awake()
    {   vegetablePanel =GameObject.Find("VegetableShop");
        Debug.Log(vegetablePanel.name);
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

                }
                break;
        }
    }

    private void OnMouseDown()
    {   Debug.Log("클릭!");
        if (state == PlotState.PLANTING)
        {
            Harvest();
        }

        if(state == PlotState.EMPTY)
        {
            OpenStorePanel();
        }

        if(state ==PlotState.LOCKED)
        {
            /*
                돈이있으면 unlock 

            */

        }
    }
    private void OpenStorePanel()
    {
        vegetablePanel.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("씨앗 상점 활성화");
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

    



