using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    public Image BlackBoard;

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
            minigameinstruction(miniGameManager.ReadGameNo());
            BlackBoard.gameObject.SetActive(false);

            miniGameManager.TogglePause(); // 미니게임의 일시 정지
        }
        else
        {
            pauseResumeButtonText.gameObject.SetActive(true);
            minigameinstruction(miniGameManager.ReadGameNo());
            BlackBoard.gameObject.SetActive(true);


            miniGameManager.TogglePause(); // 미니게임의 재개
        }
    }


    public void minigameinstruction(int minigameNO)
    {
        switch (minigameNO)
        {
            case 1:
                pauseResumeButtonText.text = "왼쪽으로 이동, 오른쪽버튼으로 점프!";
                break;
            case 2:
                pauseResumeButtonText.text = "좌우로 움직여 공을 막아라!";
                break;
            case 3:
                pauseResumeButtonText.text = "좌우로 움직여 똥을 피하라!";
                break;
            case 4:
                pauseResumeButtonText.text = "버튼을 눌러 새를 피해 날아라!";
                break;
            case 5:
                pauseResumeButtonText.text = "좌우로 움직여 안전하게 구름을 밟아라!";
                break;
            case 6:
                pauseResumeButtonText.text = "버튼을 눌러 기를 모아 점프!";
                break;
            case 7:
                pauseResumeButtonText.text = "좌우로 날아 올라 구름을 피해 날아라!";
                break;
            case 8:
                pauseResumeButtonText.text = "좌우로 높이를 조절해 벌을 피해라!";
                break;
            case 9:
                pauseResumeButtonText.text = "오른쪽버튼으로 점프! 왼쪽버튼으로 수영하라!";
                break;
            case 10:
                pauseResumeButtonText.text = "좌우로 움직이며 장애물을 피해 산을 내려가라!";
                break;
            case 11:
                pauseResumeButtonText.text = "좌우로 움직여 까마귀를 막고 젤리를 먹어라!";
                break;
            case 12:
                pauseResumeButtonText.text = "위아래로 움직이며 돌을 던져 조개를 부수고 젤리를 먹어라!";
                break;
            case 13:
                pauseResumeButtonText.text = "좌우로 움직이며 해엄쳐 돌을 피해라!";
                break;
            case 14:
                pauseResumeButtonText.text = "좌우로 나무 사이를 뛰며 돌을 피해라!";
                break;
            case 15:
                pauseResumeButtonText.text = "좌우로 움직여 날아오는 눈을 피해 젤리를 먹어라!";
                break;
            case 16:
                pauseResumeButtonText.text = "좌우로 움직이며 복어를 피해 젤리를 먹어라!";
                break;
            case 17:
                pauseResumeButtonText.text = "좌우로 움직이며 도토리를 부숴 젤리를 먹어라!";
                break;
            case 18:
                pauseResumeButtonText.text = "점프하며 구름을 밟고 다시마를 피해라! 물에 떨어지면 점프가 힘드니 주의!";
                break;
            case 19:
                pauseResumeButtonText.text = "좌우로 움직이며 꽃을 밟아 올라가라!";
                break;
            case 20:
                pauseResumeButtonText.text = "좌우로 움직이며 부서지는 발판을 주의하며 내려가라!";
                break;

        }
        
    }

}
