using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg19manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Mg19manager instance = null;

    public int level;


   

    private int score = 0; // score=jelly ���ھ ���� ������ �������� ������ �ӵ� ��������
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
            Mg19blockSpanwer spawner = FindObjectOfType<Mg19blockSpanwer>();


            if (spawner != null)
            {
                spawner.IncreaseSpeed();  


            }
        }
    }

    public void GameLevelsetting() //start and level setting
    {

        Mg19blockSpanwer spawner = FindObjectOfType<Mg19blockSpanwer>();


        for (int i = 0; i < level; i++)
        {
            spawner.IncreaseSpeed();
        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        Mg19blockSpanwer spawner = FindObjectOfType<Mg19blockSpanwer>();
        if (level != 0)
        {
            level -= 1;
            spawner.DecreaseSpeed();

        }

    }
}
