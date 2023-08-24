using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Mg13manager : MonoBehaviour
{
    public static Mg13manager instance = null;

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
        level = MiniGameManager.Instance.LoadDifficulty() - 1;
        score = MiniGameManager.Instance.LoadScore();
        GameLevelsetting();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore()
    {
        score += 1;
        MiniGameManager.Instance.IncreaseScore();

        if (score % 5 == 0)
        {
            level += 1;
            MiniGameManager.Instance.IncreaseDifficulty();

            Mg13Spawner spawner = FindObjectOfType<Mg13Spawner>();
            

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval

            }
        }
    }

    public void GameLevelsetting() //start and level setting
    {

        Mg13Spawner spawner = FindObjectOfType<Mg13Spawner>();
        Mg13jellySpawner spawner2 = FindAnyObjectByType<Mg13jellySpawner>();

        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed();
            spawner2.IncreaseSpeed();


        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        MiniGameManager.Instance.ResetScore();

        Mg13Spawner spawner = FindObjectOfType<Mg13Spawner>();
        Mg13jellySpawner spawner2 = FindAnyObjectByType<Mg13jellySpawner>();


        if (level != 0)
        {
            level -= 1;
            MiniGameManager.Instance.DecreaseDifficulty();

            spawner.DecreaseSpeed(); //Down Level
            spawner2.DecreaseSpeed();
        }

    }


}
