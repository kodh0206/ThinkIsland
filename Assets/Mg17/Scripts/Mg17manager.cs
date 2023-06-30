using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg17manager : MonoBehaviour
{
    public static Mg17manager instance = null;


    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject GameOverPanel;

    private int score = 0; // score=jelly 스코어에 이전 게임의 젤리값을 넣으면 속도 조정가능
    public bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore()
    {
        score += 1;
        scoreText.text = "Eat jelly " + score; //score= jelly

        if (score % 5 == 0)
        {
            Mg17Spawner spawner = FindObjectOfType<Mg17Spawner>();
            Mg17RockSpawner spawner2= FindAnyObjectByType<Mg17RockSpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed(); //돌 발사 속도 증가


            }
        }
    }
}
