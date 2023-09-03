using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg19manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Mg19manager instance = null;

    public bool achievementFail;


    public int level;


    Mg19blockSpanwer spawner;

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
        spawner = FindObjectOfType<Mg19blockSpanwer>();
        MiniGameManager.Instance.WriteGameNO(19);
        GameLevelsetting();

        // 초기화
		achievementFail = false;
    }

    public void Update()
    {
        // if (!achievementFail)
        // {
        //     AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
        //     if (achievementManager != null)
        //     {
        //         achievementManager.IncrementAchievement("18", 1);
        //     }
        // }
    }

    public void CheckAchievementFail()
    {
        if (!achievementFail)
        {
            AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
            if (achievementManager != null)
            {
                achievementManager.IncrementAchievement("18", 1);
            }
        }
    }

    public void AddScore()
    {
        score += 1;
        MiniGameManager.Instance.IncreaseScore();


        if (score % 5 == 0 && (level < 4))
        {
            level += 1;
            MiniGameManager.Instance.IncreaseDifficulty();


            if (spawner != null)
            {
                spawner.IncreaseSpeed();  

            }
        }
    }

    public void GameLevelsetting() //start and level setting
    {

        for (int i = 0; i < level; i++)
        {
            spawner.IncreaseSpeed();
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

        }

    }
}
