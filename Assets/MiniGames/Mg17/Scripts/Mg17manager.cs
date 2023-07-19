using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg17manager : MonoBehaviour
{
    public static Mg17manager instance = null;


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
            Mg17Spawner spawner = FindObjectOfType<Mg17Spawner>();
            Mg17RockSpawner spawner2= FindObjectOfType<Mg17RockSpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  
                spawner2.IncreaseSpeed(); 


            }
        }
    }


    public void GameLevelsetting() //start and level setting
    {

        Mg17Spawner spawner = FindObjectOfType<Mg17Spawner>();
        Mg17RockSpawner spawner2 = FindObjectOfType<Mg17RockSpawner>();


        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed();
            spawner2.IncreaseSpeed();

        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        Mg17Spawner spawner = FindObjectOfType<Mg17Spawner>();
        Mg17RockSpawner spawner2 = FindObjectOfType<Mg17RockSpawner>();

        if (level != 0)
        {
            level -= 1;

            spawner.DecreaseSpeed();
            spawner2.DecreaseSpeed(); ; //Down Level

        }

    }
}
