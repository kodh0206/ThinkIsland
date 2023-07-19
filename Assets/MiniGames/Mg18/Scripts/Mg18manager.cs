using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg18manager : MonoBehaviour
{
    public static Mg18manager instance = null;

    public int level;
    

    private int score = 0; 
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
        GameLevelsetting();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore()
    {
        score += 1;
       

        if (score % 5 == 0)
        {
            level += 1;
            Mg18ObstacleSpawner spawner = FindObjectOfType<Mg18ObstacleSpawner>();
            Mg18GroundSpawner spawner2 = FindObjectOfType<Mg18GroundSpawner>();
            Mg18jellySpawner spawner3 =FindObjectOfType<Mg18jellySpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  
                spawner2.IncreaseSpeed(); 
                spawner3.IncreaseSpeed(); 

            }
        }
    }

    public void GameLevelsetting() //start and level setting
    {

        Mg18ObstacleSpawner spawner = FindObjectOfType<Mg18ObstacleSpawner>();
        Mg18GroundSpawner spawner2 = FindObjectOfType<Mg18GroundSpawner>();
        Mg18jellySpawner spawner3 = FindObjectOfType<Mg18jellySpawner>();


        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed();
            spawner2.IncreaseSpeed();
            spawner3.IncreaseSpeed();
        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        Mg18ObstacleSpawner spawner = FindObjectOfType<Mg18ObstacleSpawner>();
        Mg18GroundSpawner spawner2 = FindObjectOfType<Mg18GroundSpawner>();
        Mg18jellySpawner spawner3 = FindObjectOfType<Mg18jellySpawner>();

        if (level != 0)
        {
            level -= 1;

            spawner.DecreaseSpeed();
            spawner2.DecreaseSpeed();
            spawner3.DecreaseSpeed();

        }

    }
}
