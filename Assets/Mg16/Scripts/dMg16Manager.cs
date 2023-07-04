using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class dMg16Manager : MonoBehaviour
{
    public static dMg16Manager instance = null;
    dMg16Jelly mg16Jelly;

    public GameObject battery;
    public GameObject fish1;
    public GameObject fish2;
    public GameObject jelly;

    // 도착 확인 변수 (bool 값)
    public bool batteryIsArrived = false;
    public bool fishIsArrived = false;
    public bool jellyIsArrived = false;
    public bool jellyIsMoving = false;


    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject GameOverPanel;

    private int score = 0;
    public bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        mg16Jelly = FindObjectOfType<dMg16Jelly>();
        // 배터리만 활성화, object 비활성화
        battery.SetActive(true);
        //battery.transform.position = new Vector2(playerPositionScript.positionPlayer, maxY);
        fish1.SetActive(false);
        fish2.SetActive(false);
        jelly.SetActive(false);
    }

    void Update()
    {
        if(batteryIsArrived)
        {
            // object 활성화
            fish1.SetActive(true);
            fish2.SetActive(true);
            jelly.SetActive(true);
            batteryIsArrived = false;
        }
        else if (fishIsArrived)
        {
            // 배터리 활성화
            battery.SetActive(true);
            fishIsArrived = false;
            jellyIsArrived = false;
        }
        
        else if (jelly.activeSelf == false && jellyIsMoving == true)
        {
            mg16Jelly.JellySetActiveFalse(); 
            jellyIsMoving = false;
            jellyIsArrived = false;
        }
    }

    public void AddScore()
    {
        score += 1;
        //MiniGameManager.Instance.AddJelly();
        if (score % 5 == 0)
        {
            dMg16FishManager fish = FindObjectOfType<dMg16FishManager>();
            dMg16Jelly jelly = FindObjectOfType<dMg16Jelly>();
            dMg16Player player = FindObjectOfType<dMg16Player>();
            dMg16Battery battery = FindObjectOfType<dMg16Battery>();

            fish.IncreaseSpeed();
            jelly.IncreaseSpeed();
            player.IncreaseSpeed();
        }
    }
}