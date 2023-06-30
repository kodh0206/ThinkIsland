using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg16Manager : MonoBehaviour
{
    public static Mg16Manager instance = null;

    //private Mg16Jelly jellyMove;


    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject GameOverPanel;

    private int score = 0;
    public bool isGameOver = false;

    //public GameObject jelly;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        //jellyMove = jelly.GetComponent<Mg16Jelly>();

        //jelly.SetActive(true);
    }

    void Update()
    {
        
    }

    public void AddScore()
    {
        score += 1;
        //MiniGameManager.Instance.AddJelly();
        
        //scoreText.text = "Eat jelly " + score; //score= jelly

        if (score % 5 == 0)
        {
            /*
            Mg12Spawner spawner = FindObjectOfType<Mg12Spawner>();
            Mg12RockSpawner spawner2= FindAnyObjectByType<Mg12RockSpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();
                spawner2.IncreaseSpeed();


            }*/
        }
    }
}
