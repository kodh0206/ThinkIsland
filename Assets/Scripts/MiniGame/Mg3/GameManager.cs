using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject GameOverPanel;
    private int score = 0; // score=jelly 스코어에 이전 게임의 젤리값을 넣으면 속도 조정가능
    public bool isGameOver = false;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore() 
    {
        score += 1;
        scoreText.text = "Eat jelly " + score; //score= jelly

        if (score % 5 == 0)
        {
            PoopSpawner spawner = FindObjectOfType <PoopSpawner> ();
            if (spawner != null) 
            {
                spawner.DecreasePoopInterval();  // decrease interval
            }
        }
    }
    public void SetGameOver()
    {
         if(isGameOver == false)
        {
            isGameOver = true;
            PoopSpawner spawner = FindObjectOfType <PoopSpawner> ();

            if (spawner != null)
            {
                spawner.StopSpawning();
            } 

            GameOverPanel.SetActive(true);
        }
    }   
    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
