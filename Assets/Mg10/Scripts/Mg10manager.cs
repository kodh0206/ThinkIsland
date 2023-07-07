using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg10manager : MonoBehaviour
{
    public static Mg10manager instance = null;



    [SerializeField]
    private GameObject GameOverPanel;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore()
    {
        score += 1;
        //scoreText.text = "Eat jelly " + score; //score= jelly

        if (score % 5 == 0)
        {
            Mg10Spawner spawner = FindObjectOfType<Mg10Spawner>();
            Mg10jellySpawner spawner2 = FindObjectOfType<Mg10jellySpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed();



            }
        }
    }
}
