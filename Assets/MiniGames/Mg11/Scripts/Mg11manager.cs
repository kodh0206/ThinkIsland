using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Mg11manager : MonoBehaviour
{
    public static Mg11manager instance = null;

    public bool achievementFail;


    public int level;

    Mg11Spawner spawner;
    Mg11jellySpawner spawner2;

    private int score = 0; // score=jelly ���ھ ���� ������ �������� ������ �ӵ� ��������
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
        MiniGameManager.Instance.WriteGameNO(11);
        level = MiniGameManager.Instance.LoadDifficulty();
        score = MiniGameManager.Instance.LoadScore();

        spawner = FindObjectOfType<Mg11Spawner>();
        spawner2 = FindObjectOfType<Mg11jellySpawner>();

        GameLevelsetting();

        // 초기화
		achievementFail = false;
    }

    public void Update()
    {
        Invoke("CheckAchievementFail", 9.5f);
    }

    public void CheckAchievementFail()
    {
        if (!achievementFail)
        {
            AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
            if (achievementManager != null)
            {
                achievementManager.IncrementAchievement("10", 1);
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

            spawner.IncreaseSpeed(); // ���� ���� ���̵��� ������ ���� ���̵� ����
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
