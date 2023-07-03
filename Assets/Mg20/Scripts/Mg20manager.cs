using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg20manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Mg20manager instance = null;


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
            Mg19blockSpanwer spawner = FindObjectOfType<Mg19blockSpanwer>();


            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // 바닥 속도 증가


            }
        }
    }
}
