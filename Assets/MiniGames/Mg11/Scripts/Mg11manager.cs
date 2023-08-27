using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Mg11manager : MonoBehaviour
{
    public static Mg11manager instance = null;

    public int level;

    Mg11Spawner spawner;
    Mg11jellySpawner spawner2;

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
        level = MiniGameManager.Instance.LoadDifficulty() - 1;
        score = MiniGameManager.Instance.LoadScore();

        spawner = FindObjectOfType<Mg11Spawner>();
        spawner2 = FindObjectOfType<Mg11jellySpawner>();

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


            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed();


            }
        }
    }



    public void GameLevelsetting() //start and level setting
    {

        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed(); // 게임 별로 난이도를 레벨에 따라 난이도 조절
            spawner2.IncreaseSpeed();
        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        MiniGameManager.Instance.ResetScore();



        if (level != 0)
        {
            level -= 1;
            MiniGameManager.Instance.DecreaseDifficulty();


            spawner.DecreaseSpeed(); //Down Level
            spawner2.DecreaseSpeed();
        }

    }

}
