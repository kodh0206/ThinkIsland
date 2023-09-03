using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class BetaManager : MonoBehaviour
{   
    
    //General
    public int money;
    public int jelly;
    public int action;
    public Button play1;
    MiniGameManager miniGame;
    GameController gameController;
    public Button RadioButton;

    public TextMeshProUGUI  moneyText;
    public TextMeshProUGUI  jellyText;
    public TextMeshProUGUI energy;

    public TextMeshProUGUI levelText; //레밸
    public TextMeshProUGUI currentExpText;
    public TextMeshProUGUI MaxExpText;
    //Farmimng
    public CropData selectPlant = null; // The selected plant
    public Field selectPlot = null; // The selected plot
    public bool isPlanting = false;
   


    public Color buyColor = Color.green;
    public Color cancelColor = Color.red;

    public bool isSelecting = false;
    public int selectedTool=0;
    // 1- water 2- Fertilizer 3- Buy plot

    public Image[] buttonsImg;
    public Sprite normalButton;
    public Sprite selectedButton;
    public StoreManager storeManager;

    public Button WaterButton;
    public bool isWaterSelected = false;

    


    void Awake()
    {   
        // 싱글톤 초기화

        miniGame =GameObject.Find("MiniGameManager").GetComponent<MiniGameManager>();
        // gameController = GameObject.Find("GameManager").GetComponent<GameController>();
    }

    private void Start()
    {
        play1.onClick.AddListener(StartMiniGame);
        RadioButton.onClick.AddListener(gotoRadio);
        money =GameController.Instance.curentgold;
        moneyText.text = money.ToString();
        jelly =GameController.Instance.currentjellyCount;
        jellyText.text = jelly.ToString();

        energy.text =GameController.Instance.currentActionPoints.ToString();
        WaterButton.onClick.AddListener(SelectWater);

        levelText.text = "LV: " + GameController.Instance.level.ToString();
        currentExpText.text = "EXP: " + GameController.Instance.current_experience.ToString() + " / ";
        MaxExpText.text = GameController.Instance.expToLevelUp[GameController.Instance.level-1].ToString();

    }   

   
    private void Update()
    {
    jelly =GameController.Instance.currentjellyCount;
    money = GameController.Instance.curentgold;
    moneyText.text = money.ToString();
    jellyText.text = jelly.ToString();
    }
    
    void StartMiniGame()
    {  if (GameController.Instance.currentActionPoints >= 20) // 활동력이 20 이상일 경우
    {
        play1.interactable = false; // 여러 클릭 방지
        AudioManager.Instance.StartMiniGame();
        miniGame.StartMiniGameWithAudio();
        GameController.Instance.currentActionPoints -= 20; // 게임 시작에 필요한 활동력 감소

        // 프로필창 업데이트 (탐험 입장)
        GameController.Instance.playMiniGame += 1;
    }
    else // 활동력이 20 미만일 경우
    {
        AudioManager.Instance.PlayError(); // 활동력 부족 효과음 재생
        // 게임 시작 못하게 하기, 필요에 따라 경고 메시지 표시 등 추가 처리 가능
    }
       
    }
    void gotoRadio()
    { 
        AudioManager.Instance.PlayPressed();
        SceneManager.LoadSceneAsync("RadioScene");
    }

        public void SelectPlant(CropData newPlant)
    {
        if(selectPlant == newPlant)
        {
            CheckSelection();
            
        }
        else
        {
            CheckSelection();
            selectPlant = newPlant;
            isPlanting = true;
        }
    }

    public void SelectTool(int toolNumber)
    {
        if(toolNumber == selectedTool)
        {
            //deselect
            CheckSelection();
        }
        else
        {
            //select tool number and check to see if anything was also selected
            CheckSelection();
            isSelecting = true;
            selectedTool = toolNumber;
            buttonsImg[toolNumber - 1].sprite = selectedButton;
        }
    }

    void CheckSelection()
    {
        if (isPlanting)
        {
            isPlanting = false;
            if (selectPlant != null)
            {   /*
                selectPlant.btnImage.color = buyColor;
                selectPlant.btnTxt.text = "Buy";
                */
                selectPlant = null;
            }
        }
        if (isSelecting)
        {
            if (selectedTool > 0)
            {
                buttonsImg[selectedTool - 1].sprite = normalButton;
            }
            isSelecting = false;
            selectedTool = 0;
        }
    }

    public void Transaction(int value)
    {
        GameController.Instance.curentgold += value;
        moneyText.text = GameController.Instance.curentgold.ToString();
        AudioManager.Instance.PlayCoin();
        // 작물 수확 합계 10000 골드 달성
        if (value > 0)
        {
            AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
            if (achievementManager != null)
            {
                achievementManager.IncrementAchievement("21", value);
            }
        }
    }

     public void SelectPlot(Field plot)
    {
        selectPlot = plot;
        storeManager.OpenStore();
    }
    public void PlantSelectedCrop()
    {
        // Ensure a crop and a plot have been selected
        if (selectPlant != null && selectPlot != null)
        {
            // Check that the player has enough money
            if (money >= selectPlant.purchasePrice)
            {
                // Subtract the cost from the player's money
                Transaction(-selectPlant.purchasePrice);

                // Plant the crop in the selected plot
                selectPlot.Plant(selectPlant);

                // Deselect the crop and plot
                selectPlant = null;
                selectPlot = null;
                AudioManager.Instance.PlayStartFarming();
                storeManager.storePanel.SetActive(false);
            }
            else
            {
                Debug.Log("Not enough money to plant this crop");
            }
        }
    }

    public void UnlockPlot(Field plot, int unlockCost)
    {
        if (money >= unlockCost)
        {
            Transaction(-unlockCost); // Pay for unlocking the plot
            plot.Unlock();
        }
        else
        {
            Debug.Log("Not enough money to unlock this plot");
        }


    }

    private void  SelectWater()
    {   Debug.Log("물 선택!");
        AudioManager.Instance.PlayWater();
        isWaterSelected = !isWaterSelected;  
    }
    public void UpdateJellyText()
    {
        jellyText.text =GameController.Instance.currentjellyCount.ToString();
    }
    public void UpdateLevelAndExpUI()
    {
    levelText.text = "LV: " + GameController.Instance.level.ToString();
    currentExpText.text = "EXP: " + GameController.Instance.current_experience.ToString() + " / ";
    MaxExpText.text = GameController.Instance.expToLevelUp[GameController.Instance.level].ToString();
    }


    public void LoadClassScene()
    {   AudioManager.Instance.PlayPressed();
        SceneManager.LoadScene("ClassScene");
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
