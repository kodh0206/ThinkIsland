using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MIniGameUI : MonoBehaviour
{   
    public MiniGameManager miniGameManager;

    public Text totalJelly;

    public Text CountDown;


    public Toggle pauseResumeButton; // 일시 정지/재개 버튼에 대한 참조
    public Text pauseResumeButtonText; // 버튼의 텍스트 변경을 위한 참조
    public static MIniGameUI Instance { get; private set; }

    public Sprite selectedSprite; // Selected 상태의 스프라이트//꺼져있음
    public Sprite defaultSprite;  // Default 상태의 스프라이트 //켜져있음
    public Image sprite;



    private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
    }
    else if (Instance != this)
    {
        Destroy(gameObject);
    }
    }
    private void Start()
    {
        // 토글에 메서드 연결
        pauseResumeButton.onValueChanged.AddListener(HandlePauseResume);
    }
    private void OnEnable()
    {
       totalJelly.text = miniGameManager.totalJelly.ToString();
       miniGameManager.minigameUIActive =true;
    }

   
 void UpdateToggleImage( bool isOn)
    {
        if(!isOn)
        {
            sprite.sprite = selectedSprite;
        }
        else
        {
            sprite.sprite = defaultSprite;
        }
    }
  
    public void UpdateJellyText()
{
    totalJelly.text = miniGameManager.totalJelly.ToString();
}

   public void HandlePauseResume(bool isToggled)
    {   UpdateToggleImage(isToggled);
        if (isToggled)
        {
            pauseResumeButtonText.gameObject.SetActive(false);
            miniGameManager.TogglePause(); // 미니게임의 일시 정지
        }
        else
        {
            pauseResumeButtonText.gameObject.SetActive(true);
            minigameinstruction(miniGameManager.ReadGameNo());
            miniGameManager.TogglePause(); // 미니게임의 재개
        }
    }


    public void minigameinstruction(int minigameNO)
    {
        switch (minigameNO)
        {
            case 1:
                pauseResumeButtonText.text = "1";
                break;
            case 2:
                pauseResumeButtonText.text = "2";
                break;
            case 3:
                pauseResumeButtonText.text = "3";
                break;
            case 4:
                pauseResumeButtonText.text = "4";
                break;
            case 5:
                pauseResumeButtonText.text = "5";
                break;
            case 6:
                pauseResumeButtonText.text = "6";
                break;
            case 7:
                pauseResumeButtonText.text = "7";
                break;
            case 8:
                pauseResumeButtonText.text = "8";
                break;
            case 9:
                pauseResumeButtonText.text = "9";
                break;
            case 10:
                pauseResumeButtonText.text = "10";
                break;
            case 11:
                pauseResumeButtonText.text = "11";
                break;
            case 12:
                pauseResumeButtonText.text = "12";
                break;
            case 13:
                pauseResumeButtonText.text = "13";
                break;
            case 14:
                pauseResumeButtonText.text = "14";
                break;
            case 15:
                pauseResumeButtonText.text = "15";
                break;
            case 16:
                pauseResumeButtonText.text = "16";
                break;
            case 17:
                pauseResumeButtonText.text = "17";
                break;
            case 18:
                pauseResumeButtonText.text = "18";
                break;
            case 19:
                pauseResumeButtonText.text = "19";
                break;
            case 20:
                pauseResumeButtonText.text = "20";
                break;

        }
        
    }

}
