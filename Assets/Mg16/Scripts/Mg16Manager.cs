using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg16Manager : MonoBehaviour
{
    public static Mg16Manager instance = null;

    public GameObject battery;
    public GameObject fish1;
    public GameObject fish2;
    public GameObject jelly;

    // 도착 확인 변수 (bool 값)
    public bool batteryIsArrived = false;
    public bool objectIsArrived = false;


    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject GameOverPanel;

    private int score = 0;
    public bool isGameOver = false;

    //public GameObject jelly;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        // 배터리만 활성화, object 비활성화
        battery.SetActive(true);
        fish1.SetActive(false);
        fish2.SetActive(false);
        jelly.SetActive(false);
    }

    void Update()
    {
        if(batteryIsArrived)
        {
            // 배터리만 비활성화, object 활성화
            fish1.SetActive(true);
            fish2.SetActive(true);
            jelly.SetActive(true);
            batteryIsArrived = false;
        }
        else if (objectIsArrived)
        {
            // 배터리만 활성화, object 비활성화
            battery.SetActive(true);
            //fish1.SetActive(false);
            //fish2.SetActive(false);
            //jelly.SetActive(false);
            objectIsArrived = false;
        }
    }

    // 배터리를 던지면 젤리+생선 등장 기능 구현 함수
    public void BatteryThrow()
    {
        // 기본 : 배터리 setActive(true), obstacle setActive(false)

        // 배터리 이동 (플레이어 위치 : y값 변화)
        // 배터리가 특정 위치에 다다르면
        // 배터리 1.5초 정지
        // setActive(false)와 동시에 obstacle setActive(true)
        // obstacle 이동 (랜덤 위치 : y값 변화)
        // obstacle 1.5초 정지
        // setActive(false)와 동시에 배터리 setActive(true)

        // 반복

    }

    public void AddScore()
    {
        score += 1;
        //MiniGameManager.Instance.AddJelly();
        
        //scoreText.text = "Eat jelly " + score; //score= jelly

        if (score % 5 == 0)
        {
            /*
            Mg12Spawner spawner = FindObjectOfType<Mg12Spawner>();
            Mg12RockSpawner spawner2= FindAnyObjectByType<Mg12RockSpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();
                spawner2.IncreaseSpeed();


            }*/
        }
    }
}
