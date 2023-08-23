using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg7manager : MonoBehaviour
{
    public static Mg7manager instance = null;


    

    [SerializeField]
    private GameObject GameOverPanel;

    public int level = 0;

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
            Mg7Spawner spawner = FindObjectOfType<Mg7Spawner>();
            Mg7jellySpanwer spawner2 = FindObjectOfType<Mg7jellySpanwer>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed();

            }
        }
    }

    public void GameLevelsetting() //start and level setting
    {

        Mg7Spawner spawner = FindObjectOfType<Mg7Spawner>();
        Mg7jellySpanwer spawner2 = FindObjectOfType<Mg7jellySpanwer>();
        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed(); // 게임 별로 난이도를 레벨에 따라 난이도 조절
            spawner2.IncreaseSpeed();
        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        Mg7Spawner spawner = FindObjectOfType<Mg7Spawner>();
        Mg7jellySpanwer spawner2 = FindObjectOfType<Mg7jellySpanwer>();


        if (level != 0)
        {
            level -= 1;

            spawner.DecreaseSpeed(); //Down Level
            spawner2.DecreaseSpeed();
        }


    }



}
