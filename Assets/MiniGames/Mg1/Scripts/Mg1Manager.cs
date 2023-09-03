using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg1Manager : MonoBehaviour
{
    public static Mg1Manager instance = null;

    public bool achievementFail = false;

    public int level;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject GameOverPanel;
    private int score = 0;
    public bool isGameOver = false;
    public bool isStunned = false;
 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        level= 1;
        MiniGameManager.Instance.WriteGameNO(1);
        level = MiniGameManager.Instance.LoadDifficulty();
        score = MiniGameManager.Instance.LoadScore();
        

        GameLevelsetting();

        achievementFail = false;

    }

    public void Update()
    {
        // // 10초 내 소 or 똥과 닿지 않았을 경우
        // if (!achievementFail)
        // {
        //     AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
        //     if (achievementManager != null)
        //     {
        //         achievementManager.IncrementAchievement("0", 1);
        //     }
        // }
    }

    public void CheckAchievementFail()
    {
        if (!achievementFail)
        {
            AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
            if (achievementManager != null)
            {
                achievementManager.IncrementAchievement("0", 1);
            }
        }
    }


    public void AddScore()
    {
        score += 1;
        //scoreText.text = "Eat jelly " + score; //score= jelly

        MiniGameManager.Instance.IncreaseScore();

        if (score % 5 == 0)
        {
            level += 1;
            MiniGameManager.Instance.IncreaseDifficulty();

            Mg1MakeJelly spawnerJelly = FindObjectOfType<Mg1MakeJelly>();
            Mg1MakePoop spawnerPoop = FindObjectOfType<Mg1MakePoop>();
            Mg1MakeCow spawnerCow = FindObjectOfType<Mg1MakeCow>();
            Mg1Jelly jelly = FindObjectOfType<Mg1Jelly>();
            Mg1Poop poop = FindObjectOfType<Mg1Poop>();
            Mg1Cow cow = FindObjectOfType<Mg1Cow>();
            
            if (spawnerJelly != null)
            {
                spawnerJelly.IncreaseSpeed();
            }
        
            if (spawnerPoop != null)
            {
                spawnerPoop.IncreaseSpeed();
            }

            if (spawnerCow != null)
            {
                spawnerCow.IncreaseSpeed();
            }
        }
    }

    /*
    public void StunPlayer()
    {
        isStunned = true;
        StartCoroutine(RecoverFromStun());
    }

    private IEnumerator RecoverFromStun()
    {
        yield return new WaitForSeconds(2f);
        isStunned = false;
    }*/


    public void GameLevelsetting() //start and level setting
    {

        Mg1MakeJelly spawnerJelly = FindObjectOfType<Mg1MakeJelly>();
        Mg1MakePoop spawnerPoop = FindObjectOfType<Mg1MakePoop>();
        Mg1MakeCow spawnerCow = FindObjectOfType<Mg1MakeCow>();

        for (int i = 0; i < level; i++)
        {
            spawnerJelly.IncreaseSpeed();
            spawnerPoop.IncreaseSpeed();
            spawnerCow.IncreaseSpeed();
        }

    }

    public void GameLevelDown() //when hit and level Down
    {

        score = 0;
        MiniGameManager.Instance.ResetScore();

        Mg1MakeJelly spawnerJelly = FindObjectOfType<Mg1MakeJelly>();
        Mg1MakePoop spawnerPoop = FindObjectOfType<Mg1MakePoop>();
        Mg1MakeCow spawnerCow = FindObjectOfType<Mg1MakeCow>();
        if (level != 0)
        {
            level -= 1;
            MiniGameManager.Instance.DecreaseDifficulty();

            spawnerJelly.DecreaseSpeed();
            spawnerPoop.DecreaseSpeed();
            spawnerCow.DecreaseSpeed();

        }

    }

}
