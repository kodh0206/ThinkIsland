using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10Spawner : MonoBehaviour
{
    public GameObject[] Mg10Obstacles;

    public GameObject jelly;

    public Transform player;



    public Transform[] obstacleSpawnPoints;
    public float obstacleRadius = 1.0f; // 장애물 반지름 설정

    public int WhatObstacle;

    [SerializeField]
    private float Mg10ObstacleSpeed = 5.0f; // 생성물의 초기 스피드

    [SerializeField]
    private float time_diff = 1.5f; // 장애물 생성 간격
    [SerializeField]
    private int minNumObstaclesToSpawn = 1; // 최소 생성 장애물 개수
    [SerializeField]
    private int maxNumObstaclesToSpawn = 1; // 최대 생성 장애물 개수

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnobstacle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawnobstacle()
    {
        

        while (true)
        {
            GameObject new_jelly = Instantiate(jelly);
            Vector2 jellyspawnPosition = GetValidSpawnPosition();
            new_jelly.transform.position = jellyspawnPosition;

            new_jelly.GetComponent<Mg10jelly>().SetSpeed(Mg10ObstacleSpeed); // 젤리의 스피드 설정
            Destroy(new_jelly, 5.0f);

            int numObstaclesToSpawn = Random.Range(minNumObstaclesToSpawn, maxNumObstaclesToSpawn + 1);

            for (int i = 0; i < numObstaclesToSpawn; i++)
            {
                int randomObstacleIndex = Random.Range(0, Mg10Obstacles.Length);
                GameObject new_Mg10Obstacle = Instantiate(Mg10Obstacles[randomObstacleIndex]);
                Vector2 spawnPosition = GetValidSpawnPosition();
                new_Mg10Obstacle.transform.position = spawnPosition;

                new_Mg10Obstacle.GetComponent<Mg10Obstacle>().SetSpeed(Mg10ObstacleSpeed);
                Destroy(new_Mg10Obstacle, 3.0f);

                yield return new WaitForSeconds(0.2f);
               
            }

        }
    }






    public void IncreaseSpeed()
    {
        Mg10ObstacleSpeed += 2.0f; // 장애물의 스피드 증가
        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("obstacle"); //Find all obstacleTag
        foreach (var groundObject in groundObjects)
        {
            groundObject.GetComponent<Mg10Obstacle>().SetSpeed(Mg10ObstacleSpeed);
        }
        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //FindAllJellyTag
        foreach (var jellyObject in jellyObjects)
        {
            jellyObject.GetComponent<Mg10jelly>().SetSpeed(Mg10ObstacleSpeed);
        }
    }

    public void DecreaseSpeed()
    {
        Mg10ObstacleSpeed -= 2.0f; // 장애물의 스피드 감소
        GameObject[] obstacleObjects = GameObject.FindGameObjectsWithTag("obstacle"); //Find all obstacleTag
        foreach (var obstacleObject in obstacleObjects)
        {
            obstacleObject.GetComponent<Mg10Obstacle>().SetSpeed(Mg10ObstacleSpeed);
        }
        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //FindAllJellyTag
        foreach (var jellyObject in jellyObjects)
        {
            jellyObject.GetComponent<Mg10jelly>().SetSpeed(Mg10ObstacleSpeed);
        }
    }

    public void GetHit()
    {
        StartCoroutine(DisableSpawning());
    }

    private IEnumerator DisableSpawning()
    {
        // 생성 멈춤
        time_diff = Mathf.Infinity;

        // 대기 시간
        yield return new WaitForSeconds(2f);

        // 생성 재개
        time_diff = 1.5f;
    }



    private Vector2 GetValidSpawnPosition()
    {
        Vector2 spawnPosition = Vector2.zero;
        bool validPosition = false;

        int whereX;

        while (!validPosition)
        {
            if (Random.value < 0.5f)
            {
                whereX = Random.Range(0,5);
                float spawnX;
                // 왼쪽에 장애물 생성
                if (whereX == 0)
                {
                    spawnX = player.position.x + -4.0f;
                }
                else if (whereX == 1)
                {
                    spawnX = player.position.x + -6.0f;
                }
                else if (whereX == 2)
                {
                    spawnX = player.position.x + -8.0f;
                }
                else if (whereX == 3)
                {
                    spawnX = player.position.x + -10.0f;
                }
                else 
                {
                    spawnX = player.position.x + -12.0f;
                }
                
                float spawnY = player.position.y - 12.5f;
                spawnPosition = new Vector2(spawnX, spawnY);
            }
            else
            {
                // 오른쪽에 장애물 생성

                whereX = Random.Range(0, 5);
                float spawnX;
                // 왼쪽에 장애물 생성
                if (whereX == 0)
                {
                    spawnX = player.position.x + 4.0f;
                }
                else if (whereX == 1)
                {
                    spawnX = player.position.x + 6.0f;
                }
                else if (whereX == 2)
                {
                    spawnX = player.position.x + 8.0f;
                }
                else if (whereX == 3)
                {
                    spawnX = player.position.x + 10.0f;
                }
                else
                {
                    spawnX = player.position.x + 12.0f;
                }
                
                float spawnY = player.position.y - 12.5f;
                spawnPosition = new Vector2(spawnX, spawnY);
            }

            // 장애물 생성 위치와 다른 장애물들과의 충돌 체크
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, obstacleRadius);
            if (colliders.Length == 0)
            {
                validPosition = true; // 충돌이 없는 유효한 위치
            }
        }

        return spawnPosition;
    }

}
