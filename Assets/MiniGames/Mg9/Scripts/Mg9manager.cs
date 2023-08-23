using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Mg9manager : MonoBehaviour
{
    public static Mg9manager instance = null;

    public int level = 0;

    

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
            Mg9Spawner spawner = FindObjectOfType<Mg9Spawner>();
            Mg9jellySpawner spawner2 = FindObjectOfType<Mg9jellySpawner>();
            Mg9SeaWeedSpawner spawner3 = FindObjectOfType<Mg9SeaWeedSpawner>();
            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed();
                spawner3.IncreaseSpeed();
            }
        }
    }

    public void GameLevelsetting() //start and level setting
    {

        Mg9Spawner spawner = FindObjectOfType<Mg9Spawner>();
        Mg9jellySpawner spawner2 = FindObjectOfType<Mg9jellySpawner>();
        Mg9SeaWeedSpawner spawner3 = FindObjectOfType<Mg9SeaWeedSpawner>();
        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed(); // 게임 별로 난이도를 레벨에 따라 난이도 조절
            spawner2.IncreaseSpeed();
            spawner3.IncreaseSpeed();
        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        Mg9Spawner spawner = FindObjectOfType<Mg9Spawner>();
        Mg9jellySpawner spawner2 = FindObjectOfType<Mg9jellySpawner>();
        Mg9SeaWeedSpawner spawner3 = FindObjectOfType<Mg9SeaWeedSpawner>();


        if (level != 0)
        {
            level -= 1;

            spawner.DecreaseSpeed(); //Down Level
            spawner2.DecreaseSpeed();
            spawner3.DecreaseSpeed();
        }


    }



}
