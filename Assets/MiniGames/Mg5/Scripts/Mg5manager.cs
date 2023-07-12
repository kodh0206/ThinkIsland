using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg5manager : MonoBehaviour
{
    public static Mg5manager instance = null;

    public int level;

    [SerializeField]
    private GameObject GameOverPanel;
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
      // 젤리 추가 및 UI 업데이트

    if (score % 5 == 0)
    {
        ObstacleSpawner spawner = FindObjectOfType<ObstacleSpawner>();

        if (spawner != null)
        {
            level += 1;
            spawner.IncreaseSpeed();
        }
    }
}


    public void GameLevelsetting() //start and level setting
    {

        ObstacleSpawner spawner = FindObjectOfType<ObstacleSpawner>();
        Mg5jellyspawner spawner2 = FindObjectOfType<Mg5jellyspawner>();
        for (int i = 0; i < level; i++)
        {

            spawner.IncreaseSpeed(); // 게임 별로 난이도를 레벨에 따라 난이도 조절
            spawner2.IncreaseSpeed();
        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        ObstacleSpawner spawner = FindObjectOfType<ObstacleSpawner>();
        Mg5jellyspawner spawner2 = FindObjectOfType<Mg5jellyspawner>();


        if (level != 0)
        {
            level -= 1;

            spawner.DecreaseSpeed(); //Down Level
            spawner2.DecreaseSpeed();
        }


    }

}
