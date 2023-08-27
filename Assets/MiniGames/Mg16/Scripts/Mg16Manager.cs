using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16Manager : MonoBehaviour
{
    public static Mg16Manager instance = null;

    public bool achievementFail;


    public int score = 0;
    private Mg16Fish1 mg16Fish1;
    private Mg16Fish2 mg16Fish2;
    private Mg16FishSpawner mg16FishSpawner;
    private Mg16Jelly mg16Jelly;
    private Mg16JellySpawner mg16JellySpawner;

    private Mg16Player mg16Player;


    public int level;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        level = 0;
        level = MiniGameManager.Instance.LoadDifficulty() - 1;
        score = MiniGameManager.Instance.LoadScore();

        mg16Fish1 = FindObjectOfType<Mg16Fish1>();
        mg16Fish2 = FindObjectOfType<Mg16Fish2>();
        mg16FishSpawner = FindObjectOfType<Mg16FishSpawner>();
        mg16Jelly = FindObjectOfType<Mg16Jelly>();
        mg16JellySpawner = FindObjectOfType<Mg16JellySpawner>();
        mg16Player = FindObjectOfType<Mg16Player>();

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
                achievementManager.IncrementAchievement("15", 1);
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

            if (mg16Fish1 != null)
            {
                mg16Fish1.IncreaseSpeed();
                mg16Fish2.IncreaseSpeed();
                mg16FishSpawner.IncreaseSpeed();
                mg16Jelly.IncreaseSpeed();
                mg16JellySpawner.IncreaseSpeed();
                mg16Player.IncreaseSpeed();
            }
        }
    }


    public void GameLevelsetting() //start and level setting
    {

        

        for (int i = 0; i < level; i++)
        {

            mg16Fish1.IncreaseSpeed();
            mg16Fish2.IncreaseSpeed();
            mg16FishSpawner.IncreaseSpeed();
            mg16Jelly.IncreaseSpeed();
            mg16JellySpawner.IncreaseSpeed();
            mg16Player.IncreaseSpeed();

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

            mg16Fish1.DecreaseSpeed();
            mg16Fish2.DecreaseSpeed();
            mg16FishSpawner.DecreaseSpeed();
            mg16Jelly.DecreaseSpeed();
            mg16JellySpawner.DecreaseSpeed();
            mg16Player.DecreaseSpeed();

        }

    }

}
