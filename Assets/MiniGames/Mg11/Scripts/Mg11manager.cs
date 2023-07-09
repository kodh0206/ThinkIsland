using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg11manager : MonoBehaviour
{
    public static Mg11manager instance = null;


    [SerializeField]
    private TextMeshProUGUI scoreText;

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
        scoreText.text = "Eat jelly " + score; //score= jelly

        if (score % 5 == 0)
        {
            Mg11Spawner spawner = FindObjectOfType<Mg11Spawner>();
            Mg11jellySpawner spawner2 = FindObjectOfType<Mg11jellySpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed();



            }
        }
    }
}
