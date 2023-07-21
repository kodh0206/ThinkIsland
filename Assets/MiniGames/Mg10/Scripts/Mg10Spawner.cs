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
    private int maxNumObstaclesToSpawn = 5; // �ִ� ���� ��ֹ� ����

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnobstacle());
    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;
        //if (time > time_diff)
        //{

        //    GameObject new_jelly = Instantiate(jelly);
        //    Vector2 jellyspawnPosition = GetValidSpawnPosition();
        //    new_jelly.transform.position = jellyspawnPosition;

        //    new_jelly.GetComponent<Mg10jelly>().SetSpeed(Mg10ObstacleSpeed); // ������ ���ǵ� ����
        //    Destroy(new_jelly, 5.0f);

        //    int numObstaclesToSpawn = Random.Range(minNumObstaclesToSpawn, maxNumObstaclesToSpawn + 1);

        //    for (int i = 0; i < numObstaclesToSpawn; i++)
        //    {
        //        WhatObstacle = Random.Range(0, 5);

        //        GameObject new_Mg10Obstacle;
        //        if (WhatObstacle == 0)
        //        { new_Mg10Obstacle = Instantiate(Mg10Obstacle1);  }
        //        else if ( WhatObstacle == 1)
        //        { new_Mg10Obstacle = Instantiate(Mg10Obstacle2); }
        //        else if ( WhatObstacle == 2)
        //        { new_Mg10Obstacle = Instantiate(Mg10Obstacle3); }
        //        else if (WhatObstacle == 3)
        //        {new_Mg10Obstacle = Instantiate(Mg10Obstacle4); }
        //        else 
        //        {  new_Mg10Obstacle = Instantiate(Mg10Obstacle5);  }

        //        Vector2 spawnPosition = GetValidSpawnPosition();
        //        new_Mg10Obstacle.transform.position = spawnPosition;


        //        new_Mg10Obstacle.GetComponent<Mg10Obstacle>().SetSpeed(Mg10ObstacleSpeed); // ��ֹ��� ���ǵ� ����
        //        Destroy(new_Mg10Obstacle, 5.0f);
        //    }

        //    time = 0;
        //}
    }

    private IEnumerator Spawnobstacle()
    {
        while (true)
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
                { new_Mg10Obstacle = Instantiate(Mg10Obstacle1); }
                else if (WhatObstacle == 1)
                { new_Mg10Obstacle = Instantiate(Mg10Obstacle2); }
                else if (WhatObstacle == 2)
                { new_Mg10Obstacle = Instantiate(Mg10Obstacle3); }
                else if (WhatObstacle == 3)
                { new_Mg10Obstacle = Instantiate(Mg10Obstacle4); }
                else
                { new_Mg10Obstacle = Instantiate(Mg10Obstacle5); }

                Vector2 spawnPosition = GetValidSpawnPosition();
                new_Mg10Obstacle.transform.position = spawnPosition;


                new_Mg10Obstacle.GetComponent<Mg10Obstacle>().SetSpeed(Mg10ObstacleSpeed); // ��ֹ��� ���ǵ� ����
                Destroy(new_Mg10Obstacle, 5.0f);
                yield return new WaitForSeconds(0.2f);
            }

        }
    }






    public void IncreaseSpeed()
    {
        Mg10ObstacleSpeed += 2.0f; // ��ֹ��� ���ǵ� ����
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
        Mg10ObstacleSpeed -= 2.0f; // ��ֹ��� ���ǵ� ����
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

        int whereX;

        while (!validPosition)
        {
            if (Random.value < 0.5f)
            {
                whereX = Random.Range(0,5);
                float spawnX;
                // ���ʿ� ��ֹ� ����
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
                // �����ʿ� ��ֹ� ����

                whereX = Random.Range(0, 5);
                float spawnX;
                // ���ʿ� ��ֹ� ����
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
