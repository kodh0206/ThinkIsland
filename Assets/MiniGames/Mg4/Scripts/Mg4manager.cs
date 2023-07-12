using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg4manager : MonoBehaviour
{
    public static Mg4manager instance = null;

    public int level; //level 

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject GameOverPanel;
    private int score = 0; // score=no death eat jelly 
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
        level= 0;
        GameLevelsetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
    score += 1;
    //MiniGameManager.Instance.AddJelly();  // 젤리 추가 및 UI 업데이트
    scoreText.text = "Eat jelly " + score;

        if (score % 5 == 0)
        {
            level += 1;
            makejelly spawner = FindObjectOfType<makejelly>();
            Makebirdpoop spawner2 = FindObjectOfType<Makebirdpoop>();

        
            if (spawner != null)
            {
                spawner.IncreaseSpeed();
                spawner2.IncreaseSpeed();
            }
        }

    }


    public void GameLevelsetting() //start and level setting
    {

        Makebirdpoop spawner = FindObjectOfType<Makebirdpoop>();
        makejelly jellspawner= FindObjectOfType<makejelly>();
        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed(); // 게임 별로 난이도를 레벨에 따라 난이도 조절
            jellspawner.IncreaseSpeed();
        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        Makebirdpoop spawner = FindObjectOfType<Makebirdpoop>();
        makejelly jellspawner = FindObjectOfType<makejelly>();

        if (level != 0)
        {
            level -= 1;

            spawner.DecreaseSpeed(); //Down Level
            jellspawner.DecreaseSpeed() ;
        }


    }



}