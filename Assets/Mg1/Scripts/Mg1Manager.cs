using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg1Manager : MonoBehaviour
{
    public static Mg1Manager instance = null;


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

    public void AddScore()
    {
        score += 1;
        scoreText.text = "Eat jelly " + score; //score= jelly

        if (score % 5 == 0)
        {
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
}
