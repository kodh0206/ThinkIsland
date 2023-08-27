using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg4manager : MonoBehaviour
{
    public static Mg4manager instance = null;

    public bool achievementFail;


    public int level; //level 

    makejelly spawner;
    Makebirdpoop spawner2;

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

        level = MiniGameManager.Instance.LoadDifficulty() - 1;
        score = MiniGameManager.Instance.LoadScore();

        spawner = FindObjectOfType<makejelly>();
        spawner2 = FindObjectOfType<Makebirdpoop>();

        GameLevelsetting();

        // 초기화
        achievementFail = false;
    }

    public void Update()
    {
        if (!achievementFail)
        {
            AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
            if (achievementManager != null)
            {
                achievementManager.IncrementAchievement("3", 1);
            }
        }
    }

    public void AddScore()
    {
    score += 1;


        MiniGameManager.Instance.IncreaseScore();

        if (score % 5 == 0 && level<3)
        {
            level += 1;
            MiniGameManager.Instance.IncreaseDifficulty();


        
            if (spawner != null)
            {
                spawner.IncreaseSpeed();
                spawner2.IncreaseSpeed();
            }
        }

    }


    public void GameLevelsetting() //start and level setting
    {

        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed(); // 게임 별로 난이도를 레벨에 따라 난이도 조절
            spawner2.IncreaseSpeed();
        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        MiniGameManager.Instance.ResetScore();

        if (level != 0)
        {
            level -= 1;
            MiniGameManager.Instance.DecreaseDifficulty();

            spawner.DecreaseSpeed(); //Down Level
            spawner2.DecreaseSpeed() ;
        }


    }



}