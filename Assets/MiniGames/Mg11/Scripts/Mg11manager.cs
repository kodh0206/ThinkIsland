using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Mg11manager : MonoBehaviour
{
    public static Mg11manager instance = null;

    public int level;



    private int score = 0; // score=jelly 스코어에 이전 게임의 젤리값을 넣으면 속도 조정가능
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
            Mg11Spawner spawner = FindObjectOfType<Mg11Spawner>();
            Mg11jellySpawner spawner2 = FindObjectOfType<Mg11jellySpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed();


            }
        }
    }



    public void GameLevelsetting() //start and level setting
    {

        Mg11Spawner spawner = FindObjectOfType<Mg11Spawner>();
        Mg11jellySpawner spawner2 = FindObjectOfType<Mg11jellySpawner>();
        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed(); // 게임 별로 난이도를 레벨에 따라 난이도 조절
            spawner2.IncreaseSpeed();
        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        Mg11Spawner spawner = FindObjectOfType<Mg11Spawner>();
        Mg11jellySpawner spawner2 = FindObjectOfType<Mg11jellySpawner>();


        if (level != 0)
        {
            level -= 1;

            spawner.DecreaseSpeed(); //Down Level
            spawner2.DecreaseSpeed();
        }

    }

}
