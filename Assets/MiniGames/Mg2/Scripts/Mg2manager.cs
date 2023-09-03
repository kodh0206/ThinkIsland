using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2manager : MonoBehaviour
{
    public static Mg2manager instance = null;

    public bool achievementFail;


    public int level;

    public int score;


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
        score = 0;
        level = 0;
        MiniGameManager.Instance.WriteGameNO(2);
        level = MiniGameManager.Instance.LoadDifficulty() ;
        score = MiniGameManager.Instance.LoadScore();
        MiniGameManager.Instance.WriteGameNO(2);
        GameLevelsetting();

        // 초기화
        achievementFail = false;
    }

    public void Update()
    {
        // // 10초 내 축구공 수비 실패 횟수가 0인 경우
        // if (!achievementFail)
        // {
        //     AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
        //     if (achievementManager != null)
        //     {
        //         achievementManager.IncrementAchievement("1", 1);
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
                achievementManager.IncrementAchievement("1", 1);
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

            Mg2ObjectManager spawner = FindObjectOfType<Mg2ObjectManager>();


            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval

            }
        }
    }


    public void GameLevelsetting() //start and level setting
    {
        Mg2ObjectManager spawner = FindObjectOfType<Mg2ObjectManager>();
        for (int i = 0; i < level; i++)
        {
            spawner.IncreaseSpeed();
        }
    }

    public void GameLevelDown() //when hit and level Down
    {
        score = 0;
        MiniGameManager.Instance.ResetScore();
        Mg2ObjectManager spawner = FindObjectOfType<Mg2ObjectManager>();

        if (level != 0)
        {
            level -= 1;

            spawner.DecreaseSpeed(); //Down Level

            MiniGameManager.Instance.DecreaseDifficulty();

        }

    }

}
