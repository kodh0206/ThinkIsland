using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg18ObstacleSpawner : MonoBehaviour
{
    public GameObject Mg18Obstacle;

    [SerializeField]
    private float Mg18ObstacleSpeed = 5.0f; // �������� �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 1.5f; // ��ֹ� ���� ����
    [SerializeField]
    private int minNumObstaclesToSpawn = 0; // �ּ� ���� ��ֹ� ����
    [SerializeField]
    private int maxNumObstaclesToSpawn = 1; // �ִ� ���� ��ֹ� ����

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
            int numObstaclesToSpawn = Random.Range(minNumObstaclesToSpawn, maxNumObstaclesToSpawn + 1);

            for (int i = 0; i < numObstaclesToSpawn; i++)
            {
                GameObject new_Mg18Obstacle = Instantiate(Mg18Obstacle);

                // ��ǥ�� �����ϰ� �����Ͽ� ����
                Vector2 spawnPosition = GetRandomSpawnPosition();
                new_Mg18Obstacle.transform.position = spawnPosition;

                new_Mg18Obstacle.GetComponent<Mg18Obstacle>().SetSpeed(Mg18ObstacleSpeed); // ��ֹ��� ���ǵ� ����
                Destroy(new_Mg18Obstacle, 5.0f);
            }

            time = 0;
        }
    }


    private Vector2 GetRandomSpawnPosition()
    {
        // ������ ��ġ �ε��� ����
        int randomIndex = Random.Range(0, 1);

        // �̸� ���ǵ� ��ġ�� �迭
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(13f, Random.Range(-4f, -1f)),
        //new Vector2(13f, 0f),
        //new Vector2(13f, 1.5f),
        //new Vector2(13f, 3f),
        };

        // ���õ� ������ ��ġ ��ȯ
        return spawnPositions[randomIndex];
    }
    public void IncreaseSpeed()
    {
        Mg18ObstacleSpeed += 2.0f; // ��ֹ��� ���ǵ� ����
        
    }
}