using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg20manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Mg20manager instance = null;

    public int level;


    private int score = 0; 
   

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
        level = 1;
        GameLevelsetting();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore()
    {
        score += 1;
        

        if (score % 5 == 0 && (level <4))
        {
            Mg20BlockSpawner spawner = FindObjectOfType<Mg20BlockSpawner>();
            Mg20ChimneyMove chimney = FindObjectOfType<Mg20ChimneyMove>();
            level += 1;

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // �ٴ� �ӵ� ����
                chimney.IncreaseSpeed();

            }
        }
    }

    public void GameLevelsetting() //start and level setting
    {

        Mg20BlockSpawner spawner = FindObjectOfType<Mg20BlockSpawner>();
        Mg20ChimneyMove chimney = FindObjectOfType<Mg20ChimneyMove>();

        for (int i = 0; i < level; i++)
        {
            spawner.IncreaseSpeed();
            chimney.IncreaseSpeed();
        }

        

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        Mg20BlockSpawner spawner = FindObjectOfType<Mg20BlockSpawner>();
        Mg20ChimneyMove chimney = FindObjectOfType<Mg20ChimneyMove>();
        if (level != 0)
        {
            level -= 1;
            spawner.DecreaseSpeed();
            chimney.DecreaseSpeed();

        }

    }
}
