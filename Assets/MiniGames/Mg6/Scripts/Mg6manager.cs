using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg6manager : MonoBehaviour
{
    public static Mg6manager instance = null;

    public bool achievementFail;



    Mg6Spawner spawner;
    Mg6JellySpawner spawner2;


    public int level;

    private int score = 0; 
    

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
        level = 1;
        MiniGameManager.Instance.WriteGameNO(6);
        level = MiniGameManager.Instance.LoadDifficulty();
        score = MiniGameManager.Instance.LoadScore();

        spawner = FindObjectOfType<Mg6Spawner>();
        spawner2 = FindObjectOfType<Mg6JellySpawner>();

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
        //         achievementManager.IncrementAchievement("5", 1);
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
                achievementManager.IncrementAchievement("5", 1);
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
                spawner.IncreaseSpeed();  // decrease interval
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
            spawner2.DecreaseSpeed();
        }


    }


}
