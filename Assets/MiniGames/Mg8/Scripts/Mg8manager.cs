using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Mg8manager : MonoBehaviour
{
    public static Mg8manager instance = null;

    public int level;

    [SerializeField]
    private GameObject GameOverPanel;

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
        level = MiniGameManager.Instance.LoadDifficulty() - 1;
        score = MiniGameManager.Instance.LoadScore();

        GameLevelsetting();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore()
    {
        score += 1;
        MiniGameManager.Instance.IncreaseScore();

        if (score % 5 == 0)
        {

            level += 1;
            MiniGameManager.Instance.IncreaseDifficulty();

            Mg8Spawner spawner = FindObjectOfType<Mg8Spawner>();
            Mg8jellySpawner spawner2 = FindObjectOfType<Mg8jellySpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed(); 



            }
        }
    }


    public void GameLevelsetting() //start and level setting
    {

        Mg8Spawner spawner = FindObjectOfType<Mg8Spawner>();
        Mg8jellySpawner spawner2 = FindObjectOfType<Mg8jellySpawner>();
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

        Mg8Spawner spawner = FindObjectOfType<Mg8Spawner>();
        Mg8jellySpawner spawner2 = FindObjectOfType<Mg8jellySpawner>();


        if (level != 0)
        {
            level -= 1;
            MiniGameManager.Instance.DecreaseDifficulty();

            spawner.DecreaseSpeed(); //Down Level
            spawner2.DecreaseSpeed();
        }


    }

}
