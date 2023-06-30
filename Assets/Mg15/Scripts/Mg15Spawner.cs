using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg15Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Mg15Obstacle;

    public GameObject jelly;


    [SerializeField]
    private float Mg15ObstacleSpeed = 1.0f; // �������� �ʱ� �߷� ��� �ӵ�

    [SerializeField]
    private float Mg15jellySpeed = 1.0f; // �������� �ʱ� �߷� ��� �ӵ�

    [SerializeField]
    private float time_diff = 2.5f; // ��ֹ� ���� ����
    [SerializeField]
    private int minNumObstaclesToSpawn = 3; // �ּ� ���� ��ֹ� ����
    [SerializeField]
    private int maxNumObstaclesToSpawn = 5; // �ִ� ���� ��ֹ� ����

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



                if (Random.value < 0.7f)
                {
                    GameObject new_Mg15Obstacle = Instantiate(Mg15Obstacle);
                    // �����ϰ� ��ġ ����
                    Vector2 spawnPosition = GetRandomSpawnPosition();
                    new_Mg15Obstacle.transform.position = spawnPosition;
                    new_Mg15Obstacle.GetComponent<Mg15Obstacle>().SetSpeed(Mg15ObstacleSpeed); // ��ֹ��� ���ǵ� ����

                    Destroy(new_Mg15Obstacle, 5.0f);
                }
                else
                {
                    GameObject new_jelly = Instantiate(jelly);
                    // �����ϰ� ��ġ ����
                    Vector2 spawnPosition = GetRandomSpawnPosition();
                    new_jelly.transform.position = spawnPosition;
                    new_jelly.GetComponent<Mg15jelly>().SetSpeed(Mg15jellySpeed); // ��ֹ��� ���ǵ� ����

                    Destroy(new_jelly, 5.0f);

                }
            }

            time = 0;
        }
    }


    private Vector2 GetRandomSpawnPosition()
    {
        // ������ ��ġ �ε��� ����
        int randomIndex = Random.Range(0, 6);

        // �̸� ���ǵ� ��ġ�� �迭
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(1.5f, 0.6f),
        new Vector2(3.5f, 0.6f),
        new Vector2(5.5f, 0.6f),
        new Vector2(-1.5f, 0.6f),
        new Vector2(-3.5f, 0.6f),
        new Vector2(-5.5f, 0.6f)
        };

        // ���õ� ������ ��ġ ��ȯ
        return spawnPositions[randomIndex];
    }

    public void IncreaseSpeed()
    {
        minNumObstaclesToSpawn += 1;
        maxNumObstaclesToSpawn += 1;
        time_diff -= 0.5f; // ��ֹ��� ���� ���� ����
    }


}
