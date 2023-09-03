using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MiniGame3Manager : MonoBehaviour
{
    public static MiniGame3Manager instance = null;

    public bool achievementFail;
    
    private int score = 0;

    public int level; //level 

    public bool isStunned = false; // 물체가 현재 스턴 상태인지를 나타내는 부울 값입니다.

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        level = 0; // level setting
        MiniGameManager.Instance.WriteGameNO(3);
        level =MiniGameManager.Instance.LoadDifficulty();
        score= MiniGameManager.Instance.LoadScore();

        GameLevelsetting();

        // 초기화
        achievementFail = false;
    }

    public void Update()
    {
        // 10초 내 똥과 닿지 않았을 경우
        if (!achievementFail)
        {
            AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
            if (achievementManager != null)
            {
                achievementManager.IncrementAchievement("2", 1);
            }
        }
    }

    public void CheckAchievementFail()
    {
        if (!achievementFail)
        {
            AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
            if (achievementManager != null)
            {
                achievementManager.IncrementAchievement("2", 1);
            }
        }
    }

    public void AddScore() 
    {
        score += 1; // for level Controll

       MiniGameManager.Instance.IncreaseScore();

        if (score % 5 == 0)
        {
            PoopSpawner spawner = FindObjectOfType <PoopSpawner> ();
            if (spawner != null) 
            {
                level += 1;
                MiniGameManager.Instance.IncreaseDifficulty();

                spawner.DecreasePoopInterval();  // decrease interval

            }
        }
    }


    public void GameLevelsetting() //start and level setting
    {

        PoopSpawner spawner = FindObjectOfType<PoopSpawner>();
        for (int i = 0; i < level; i++)
        {
            
            spawner.DecreasePoopInterval(); // 게임 별로 난이도를 레벨에 따라 난이도 조절
            

        }

    }

    public void GameLevelDown() //when hit and level Down
    {
        
        score = 0;

        MiniGameManager.Instance.ResetScore();

        PoopSpawner spawner = FindObjectOfType<PoopSpawner>();
        spawner.GetHit(); // stop to spawn

        if (level != 0)
        {
            level -= 1;
            MiniGameManager.Instance.DecreaseDifficulty();

            spawner.IncreasePoopInterval(); //Down Level

            

        }
        
       
    }


    
    public void StunPlayer()
    {
        isStunned = true;
        StartCoroutine(RecoverFromStun());
    }

    private IEnumerator RecoverFromStun()
    {
        yield return new WaitForSeconds(2f);
        isStunned = false;
    }
    
}