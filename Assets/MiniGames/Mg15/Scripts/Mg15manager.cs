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
        Mg15Spawner spawner = FindObjectOfType<Mg15Spawner>();

        if (level != 0)
        {
            level -= 1;

            spawner.DecreaseSpeed(); //Down Level
            
        }

    }
}
