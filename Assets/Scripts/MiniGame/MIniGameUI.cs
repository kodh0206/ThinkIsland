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
            miniGameManager.TogglePause(); // 미니게임의 재개
        }
    }

}
