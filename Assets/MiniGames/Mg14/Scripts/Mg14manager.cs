using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Mg14manager : MonoBehaviour
{
    public static Mg14manager instance = null;


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
            Mg14Spawner spawner = FindObjectOfType<Mg14Spawner>();


            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval

            }
        }
    }


    public void GameLevelsetting() //start and level setting
    {

        Mg14Spawner spawner = FindObjectOfType<Mg14Spawner>();
        Mg14jellySpawner spawner2 = FindAnyObjectByType<Mg14jellySpawner>();

        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed();
            spawner2.IncreaseSpeed();


        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        Mg14Spawner spawner = FindObjectOfType<Mg14Spawner>();
        Mg14jellySpawner spawner2 = FindAnyObjectByType<Mg14jellySpawner>();


        if (level != 0)
        {
            level -= 1;

            spawner.DecreaseSpeed(); //Down Level
            spawner2.DecreaseSpeed();
        }

    }

}
