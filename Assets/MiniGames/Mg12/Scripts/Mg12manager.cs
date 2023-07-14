using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg12manager : MonoBehaviour
{
    public static Mg12manager instance = null;

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


    public void AddScore()
    {
        score += 1;

        if (score % 5 == 0)
        {
            level += 1;
            Mg12Spawner spawner = FindObjectOfType<Mg12Spawner>();
            Mg12RockSpawner spawner2= FindAnyObjectByType<Mg12RockSpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed(); 


            }
        }
    }


    public void GameLevelsetting() //start and level setting
    {

        Mg12Spawner spawner = FindObjectOfType<Mg12Spawner>();
        Mg12RockSpawner spawner2 = FindAnyObjectByType<Mg12RockSpawner>();

        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed();
            spawner2.IncreaseSpeed();


        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        Mg12Spawner spawner = FindObjectOfType<Mg12Spawner>();
        Mg12RockSpawner spawner2 = FindAnyObjectByType<Mg12RockSpawner>();


        if (level != 0)
        {
            level -= 1;

            spawner.DecreaseSpeed(); //Down Level
            spawner2.IncreaseSpeed();
        }

    }


}
