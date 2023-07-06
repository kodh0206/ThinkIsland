using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg8Spawner : MonoBehaviour
{
    public GameObject Mg8Obstacle;
    public GameObject Mg8Obstacle2;
    public GameObject Mg8Obstacle3;
    public GameObject Mg8Obstacle4;

    [SerializeField]
    private float Mg8ObstacleSpeed = 5.0f; // �������� �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 1.5f; // ��ֹ� ���� ����
    [SerializeField]
    private int minNumObstaclesToSpawn = 1; // �ּ� ���� ��ֹ� ����
    [SerializeField]
    private int maxNumObstaclesToSpawn = 1; // �ִ� ���� ��ֹ� ����

    float time = 0;

    public int WhatObstacle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > time_diff)
        {
            int numObstaclesToSpawn = Random.Range(minNumObstaclesToSpawn, maxNumObstaclesToSpawn + 1);

            for (int i = 0; i < numObstaclesToSpawn; i++)
            {
                

                WhatObstacle = Random.Range(0, 5);

                GameObject new_Mg8Obstacle;
                if (WhatObstacle == 0)
                {
                    new_Mg8Obstacle = Instantiate(Mg8Obstacle);
                }

                else if (WhatObstacle == 1)
                {
                    new_Mg8Obstacle = Instantiate(Mg8Obstacle2);
                }
                else if (WhatObstacle == 2)
                {
                    new_Mg8Obstacle = Instantiate(Mg8Obstacle3);
                }
                else
                {
                    new_Mg8Obstacle = Instantiate(Mg8Obstacle4);
                }


                // ��ǥ�� ����
                Vector2 spawnPosition = new Vector2(9.4f, -0.5f);
                new_Mg8Obstacle.transform.position = spawnPosition;

                //new_Mg8Obstacle.GetComponent<Mg8Obstacle>().SetSpeed(Mg8ObstacleSpeed); // ��ֹ��� ���ǵ� ����
                Destroy(new_Mg8Obstacle, 5.0f);
            }

            time = 0;
        }
    }

    public void IncreaseSpeed()
    {
       
        time_diff -= 0.1f; // ��ֹ��� ���� ���� ����
    }
}
