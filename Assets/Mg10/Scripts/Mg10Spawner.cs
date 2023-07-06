using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10Spawner : MonoBehaviour
{
    public GameObject Mg10Obstacle1;
    public GameObject Mg10Obstacle2;
    public GameObject Mg10Obstacle3;
    public GameObject Mg10Obstacle4;
    public GameObject Mg10Obstacle5;

    public GameObject jelly;

    public Transform player;



    public Transform[] obstacleSpawnPoints;
    public float obstacleRadius = 1.0f; // ��ֹ� ������ ����

    public int WhatObstacle;

    [SerializeField]
    private float Mg10ObstacleSpeed = 5.0f; // �������� �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 1.5f; // ��ֹ� ���� ����
    [SerializeField]
    private int minNumObstaclesToSpawn = 3; // �ּ� ���� ��ֹ� ����
    [SerializeField]
    private int maxNumObstaclesToSpawn = 6; // �ִ� ���� ��ֹ� ����

    float time = 0;

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

            GameObject new_jelly = Instantiate(jelly);
            Vector2 jellyspawnPosition = GetValidSpawnPosition();
            new_jelly.transform.position = jellyspawnPosition;

            new_jelly.GetComponent<Mg10jelly>().SetSpeed(Mg10ObstacleSpeed); // ������ ���ǵ� ����
            Destroy(new_jelly, 5.0f);

            int numObstaclesToSpawn = Random.Range(minNumObstaclesToSpawn, maxNumObstaclesToSpawn + 1);

            for (int i = 0; i < numObstaclesToSpawn; i++)
            {
                WhatObstacle = Random.Range(0, 5);

                GameObject new_Mg10Obstacle;
                if (WhatObstacle == 0)
                {
                    new_Mg10Obstacle = Instantiate(Mg10Obstacle1);
                }

                else if ( WhatObstacle == 1)
                {
                    new_Mg10Obstacle = Instantiate(Mg10Obstacle2);
                }
                else if ( WhatObstacle == 2)
                {
                    new_Mg10Obstacle = Instantiate(Mg10Obstacle3);
                }
                else if (WhatObstacle == 3)
                {
                    new_Mg10Obstacle = Instantiate(Mg10Obstacle4);
                }
                else 
                {
                    new_Mg10Obstacle = Instantiate(Mg10Obstacle5);
                }


                Vector2 spawnPosition = GetValidSpawnPosition();
                new_Mg10Obstacle.transform.position = spawnPosition;


                new_Mg10Obstacle.GetComponent<Mg10Obstacle>().SetSpeed(Mg10ObstacleSpeed); // ��ֹ��� ���ǵ� ����
                Destroy(new_Mg10Obstacle, 5.0f);
            }

            time = 0;
        }
    }

    public void IncreaseSpeed()
    {
        Mg10ObstacleSpeed += 2.0f; // ��ֹ��� ���ǵ� ����
        time_diff -= 0.1f; // ��ֹ��� ���� ���� ����
    }

    public void GetHit()
    {
        StartCoroutine(DisableSpawning());
    }

    private IEnumerator DisableSpawning()
    {
        // ���� ����
        time_diff = Mathf.Infinity;

        // ��� �ð�
        yield return new WaitForSeconds(2f);

        // ���� �簳
        time_diff = 1.5f;
    }



    private Vector2 GetValidSpawnPosition()
    {
        Vector2 spawnPosition = Vector2.zero;
        bool validPosition = false;

        while (!validPosition)
        {
            if (Random.value < 0.5f)
            {
                // ���ʿ� ��ֹ� ����
                float spawnX = player.position.x + Random.Range(-10.0f, -3.0f);
                float spawnY = player.position.y - 12.5f;
                spawnPosition = new Vector2(spawnX, spawnY);
            }
            else
            {
                // �����ʿ� ��ֹ� ����
                float spawnX = player.position.x + Random.Range(3f, 10.0f);
                float spawnY = player.position.y - 12.5f;
                spawnPosition = new Vector2(spawnX, spawnY);
            }

            // ��ֹ� ���� ��ġ�� �ٸ� ��ֹ������ �浹 üũ
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, obstacleRadius);
            if (colliders.Length == 0)
            {
                validPosition = true; // �浹�� ���� ��ȿ�� ��ġ
            }
        }

        return spawnPosition;
    }

}
