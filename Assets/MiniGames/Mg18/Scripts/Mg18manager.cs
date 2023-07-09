using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg18manager : MonoBehaviour
{
    public static Mg18manager instance = null;


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
            Mg18ObstacleSpawner spawner = FindObjectOfType<Mg18ObstacleSpawner>();
            Mg18GroundSpawner spawner2 = FindAnyObjectByType<Mg18GroundSpawner>();
            Mg18jellySpawner spawner3 =FindAnyObjectByType<Mg18jellySpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // ��ֹ� �ӵ� ����
                spawner2.IncreaseSpeed(); //�� �ӵ� ����
                spawner3.IncreaseSpeed(); //���� �ӵ�����

            }
        }
    }
}
