using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Mg10manager : MonoBehaviour
{
    public static Mg10manager instance = null;

    public bool achievementFail;


    public int level;

    Mg10Spawner spawner;
    Mg10jellySpawner spawner2;

    private int score = 0; // score=jelly 
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

        spawner = FindObjectOfType<Mg10Spawner>();
        spawner2 = FindObjectOfType<Mg10jellySpawner>();

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
        //         achievementManager.IncrementAchievement("9", 1);
        //     }
        // }
    }

    public void AddScore()
    {
        score += 1;
        MiniGameManager.Instance.IncreaseScore();
        //scoreText.text = "Eat jelly " + score; //score= jelly

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
