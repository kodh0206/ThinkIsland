using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Mg10manager : MonoBehaviour
{
    public static Mg10manager instance = null;

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
        //scoreText.text = "Eat jelly " + score; //score= jelly

        if (score % 5 == 0)
        {
            level += 1;
            Mg10Spawner spawner = FindObjectOfType<Mg10Spawner>();
            Mg10jellySpawner spawner2 = FindObjectOfType<Mg10jellySpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed();

            }
        }
    }

    public void GameLevelsetting() //start and level setting
    {

        Mg10Spawner spawner = FindObjectOfType<Mg10Spawner>();
        Mg10jellySpawner spawner2 = FindObjectOfType<Mg10jellySpawner>();
        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed(); // 게임 별로 난이도를 레벨에 따라 난이도 조절
            spawner2.IncreaseSpeed();
        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        Mg10Spawner spawner = FindObjectOfType<Mg10Spawner>();
        Mg10jellySpawner spawner2 = FindObjectOfType<Mg10jellySpawner>();


        if (level != 0)
        {
            level -= 1;

            spawner.DecreaseSpeed(); //Down Level
            spawner2.DecreaseSpeed();
        }

    }


}
