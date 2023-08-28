using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg18manager : MonoBehaviour
{
    public static Mg18manager instance = null;

    public bool achievementFail;


    public int level;

    Mg18ObstacleSpawner spawner;
    Mg18GroundSpawner spawner2;
    Mg18jellySpawner spawner3;
    Mg18MovingBack BackGround;

    private int score = 0; 
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
        level = 0;
        level = MiniGameManager.Instance.LoadDifficulty();
        score = MiniGameManager.Instance.LoadScore();
        Mg18ObstacleSpawner spawner = FindObjectOfType<Mg18ObstacleSpawner>();
        Mg18GroundSpawner spawner2 = FindAnyObjectByType<Mg18GroundSpawner>();
        Mg18jellySpawner spawner3 = FindAnyObjectByType<Mg18jellySpawner>();
        Mg18MovingBack BackGround = FindObjectOfType<Mg18MovingBack>();

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
                achievementManager.IncrementAchievement("17", 1);
            }
        }
    }

    public void AddScore()
    {
        score += 1;
        MiniGameManager.Instance.IncreaseScore();

        if (score % 5 == 0)
        {
            level += 1;
            MiniGameManager.Instance.IncreaseDifficulty();



            if (spawner != null)
            {
                spawner.IncreaseSpeed();  
                spawner2.IncreaseSpeed(); 
                spawner3.IncreaseSpeed();
                BackGround.IncreaseSpeed();
            }
        }
    }

    public void GameLevelsetting() //start and level setting
    {



        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed();
            spawner2.IncreaseSpeed();
            spawner3.IncreaseSpeed();
            BackGround.IncreaseSpeed();

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

            spawner.DecreaseSpeed();
            spawner2.DecreaseSpeed();
            spawner3.DecreaseSpeed();
            BackGround.DecreaseSpeed() ;

        }

    }
}
