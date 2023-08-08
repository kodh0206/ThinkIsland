using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Field : MonoBehaviour
{
    public enum PlotState {LOCKED, EMPTY, PLANTING,HARVEST};
    public PlotState state = PlotState.EMPTY;
    public Sprite lockedSprite;
    public Sprite emptySprite;
    public CropData currentCropData;
    public SpriteRenderer plotSprite;
    public SpriteRenderer cropSprite;
    private float timeLeft;
    private int stage;
    private ES3File saveFile;
    public BetaManager fm;

    bool isDry =true; //처음에는 땅이 말라져있음
    public GameObject vegetablePanel;
    private void Awake()
    {   string uniqueFilename = $"FieldData_{GetInstanceID()}";
        saveFile = new ES3File(uniqueFilename);

        // Load data if exists
        if (saveFile.KeyExists("state"))
        {
            // Check time difference between last saved time and now
            System.TimeSpan timeDifference = System.DateTime.Now - saveFile.Load<System.DateTime>("lastSavedTime");

            // Load saved data
            state = saveFile.Load<PlotState>("state");
            string cropDataName = saveFile.Load<string>("currentCropData", null); // null을 기본값으로 설정합니다.
            if(cropDataName != null)
                currentCropData = Resources.Load<CropData>(cropDataName);
            else
                currentCropData = null; // 또는 기본값 설정
            
            timeLeft = saveFile.Load<float>("timeLeft") - (float)timeDifference.TotalSeconds;
            stage = saveFile.Load<int>("stage");
            isDry = saveFile.Load<bool>("isDry");
        }

        else
        {

        state = PlotState.LOCKED;
        currentCropData = null;
        timeLeft = 0;
        stage = 0;
        isDry = true;

        }
        fm = FindObjectOfType<BetaManager>();
        Debug.Assert(fm != null, "BetaManager not found");

        vegetablePanel = GameObject.Find("FarmStore");
        Debug.Assert(vegetablePanel != null, "FarmStore object not found");
        plotSprite = GetComponent<SpriteRenderer>();
        cropSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        ChangeState(state);

    }

    private void Update()
    {

        Debug.Log($"saveFile: {saveFile}, currentCropData: {currentCropData}");
    switch (state)
        {
            case PlotState.EMPTY:
                Debug.Log("비어있음");
                break;
            case PlotState.PLANTING:
            if(!isDry)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {   
                    stage++;
                    if (stage < currentCropData.growProgressSprites.Length)
                    {
                        timeLeft = currentCropData.TimesToGrow / currentCropData.growProgressSprites.Length;
                        cropSprite.sprite = currentCropData.growProgressSprites[stage-1];
                    }
                    else
                    {
                        state = PlotState.HARVEST;
                        cropSprite.sprite = currentCropData.growProgressSprites[stage-1];
                    }
                }
            }
            else
            {
                Debug.Log("물좀.....");
            }
                break;
        }
    
    }

    private void OnMouseDown()
    {   
    Debug.Log("클릭!"+GetInstanceID());
     if(state == PlotState.LOCKED)
    {
        fm.UnlockPlot(this, 200); // Unlock this plot for 200 dollars
        Debug.Log("토지해금");
    }
    if(state == PlotState.EMPTY)
    {   fm.selectPlot =this;
        Debug.Log("선택한 타일 이름"+this.name);
        OpenStorePanel();
    }
    else if(state == PlotState.HARVEST)
    {       Debug.Log("수확 완료!");
            Harvest();
    }

    if(fm.isWaterSelected)
    {
        if(isDry)
        {
           GiveWater();
            Debug.Log("아 시원해");

        }   
    }
    }


    private void OpenStorePanel()
    {
        vegetablePanel.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("씨앗 상점 활성화");
    }
   public void Plant(CropData cropData)
{
    if (state == PlotState.EMPTY)
    {
        state = PlotState.PLANTING;
        currentCropData = cropData;
        timeLeft = cropData.TimesToGrow;
        stage = 0;
        cropSprite.sprite = cropData.growProgressSprites[stage];
    }
}

    private void Harvest()
    {
        if (state == PlotState.HARVEST)
        {
            fm.Transaction(currentCropData.sellPrice);
            currentCropData = null;
            cropSprite.sprite = null;
            isDry = true;
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
            case PlotState.HARVEST:
                plotSprite.sprite = cropSprite.sprite = currentCropData.growProgressSprites[stage];
                break;
        }
    }

    public void Unlock()
{
    if (state == PlotState.LOCKED)
    {
        ChangeState(PlotState.EMPTY);
    }
}

public void GiveWater()
{
   isDry=false;
}

private void OnEnable()
{
    // Add our event handler to the scene loaded event
    SceneManager.sceneLoaded += OnSceneLoaded;
}

private void OnDisable()
{
    // Remove our event handler from the scene loaded event
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    // A scene has been loaded, load state
    LoadState();
}

private void OnApplicationQuit()
{
    // The application is quitting, save state
    SaveState();
}
private void SaveState()
{
    // Save data on application quit
    saveFile.Save("state", state);
    if (currentCropData != null)
    {
        saveFile.Save("currentCropData", currentCropData.plantName);
    }
    saveFile.Save("timeLeft", timeLeft);
    saveFile.Save("stage", stage);
    saveFile.Save("isDry", isDry);
    saveFile.Save("lastSavedTime", System.DateTime.Now);

    // Close the file
    saveFile.Sync();
}

private void LoadState()
{
    // Load data if exists
    if (saveFile.KeyExists("state"))
    {
        // Check time difference between last saved time and now
        System.TimeSpan timeDifference = System.DateTime.Now - saveFile.Load<System.DateTime>("lastSavedTime");

        // Load saved data
        state = saveFile.Load<PlotState>("state");
        string cropDataName = saveFile.Load<string>("currentCropData");
        currentCropData = Resources.Load<CropData>(cropDataName);
        timeLeft = saveFile.Load<float>("timeLeft") - (float)timeDifference.TotalSeconds;
        stage = saveFile.Load<int>("stage");
        isDry = saveFile.Load<bool>("isDry");
    }
}




}

    



