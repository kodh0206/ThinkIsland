using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg6manager : MonoBehaviour
{
    public static Mg6manager instance = null;



    

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

            Mg6Spawner spawner = FindObjectOfType<Mg6Spawner>();
            Mg6JellySpawner spawner2 = FindObjectOfType<Mg6JellySpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed();


            }
        }
    }



    public void GameLevelsetting() //start and level setting
    {

        Mg6Spawner spawner = FindObjectOfType<Mg6Spawner>();
        Mg6JellySpawner spawner2 = FindObjectOfType<Mg6JellySpawner>();
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

        Mg6Spawner spawner = FindObjectOfType<Mg6Spawner>();
        Mg6JellySpawner spawner2 = FindObjectOfType<Mg6JellySpawner>();


        if (level != 0)
        {
            level -= 1;
            MiniGameManager.Instance.DecreaseDifficulty();

            spawner.DecreaseSpeed(); //Down Level
            spawner2.DecreaseSpeed();
        }


    }


}
