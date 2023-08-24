using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg15manager : MonoBehaviour
{
    public static Mg15manager instance = null;

    public int level;


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

            Mg15Spawner spawner = FindObjectOfType<Mg15Spawner>();


            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval

            }
        }
    }


    public void GameLevelsetting() //start and level setting
    {

        Mg15Spawner spawner = FindObjectOfType<Mg15Spawner>();
        

        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed();

        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        MiniGameManager.Instance.ResetScore();

        Mg15Spawner spawner = FindObjectOfType<Mg15Spawner>();

        if (level != 0)
        {
            level -= 1;
            MiniGameManager.Instance.DecreaseDifficulty();

            spawner.DecreaseSpeed(); //Down Level
            
        }

    }
}
