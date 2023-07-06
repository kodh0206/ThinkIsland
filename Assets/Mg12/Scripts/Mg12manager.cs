using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mg12manager : MonoBehaviour
{
    public static Mg12manager instance = null;




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


    public void AddScore()
    {
        score += 1;

        if (score % 5 == 0)
        {
            Mg12Spawner spawner = FindObjectOfType<Mg12Spawner>();
            Mg12RockSpawner spawner2= FindAnyObjectByType<Mg12RockSpawner>();

            if (spawner != null)
            {
                spawner.IncreaseSpeed();  // decrease interval
                spawner2.IncreaseSpeed(); //�� �߻� �ӵ� ����


            }
        }
    }
}
