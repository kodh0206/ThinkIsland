using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg7manager : MonoBehaviour
{
    public static Mg7manager instance = null;


    public bool achievementFail;


    [SerializeField]
    private GameObject GameOverPanel;

    public int level = 0;

    private int score = 0; 
    public bool isGameOver = false;

    Mg7Spawner spawner;
    Mg7jellySpanwer spawner2;

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
        MiniGameManager.Instance.WriteGameNO(7);
        level = MiniGameManager.Instance.LoadDifficulty();
        score = MiniGameManager.Instance.LoadScore();
        spawner = FindObjectOfType<Mg7Spawner>();
        spawner2 = FindObjectOfType<Mg7jellySpanwer>();

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
        //         achievementManager.IncrementAchievement("6", 1);
        //     }
        // }
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
