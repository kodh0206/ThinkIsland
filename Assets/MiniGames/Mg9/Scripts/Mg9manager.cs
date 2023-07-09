using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg9manager : MonoBehaviour
{
    public static Mg9manager instance = null;



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
        if (score % 5 == 0)
        {
            Mg9Spawner spawner = FindObjectOfType<Mg9Spawner>();
            Mg9jellySpawner spawner2 = FindObjectOfType<Mg9jellySpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed();



            }
        }
    }
}
