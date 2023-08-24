using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2manager : MonoBehaviour
{
    public static Mg2manager instance = null;

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
