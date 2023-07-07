using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg11Spawner : MonoBehaviour
{
    public GameObject Mg11Obstacle;
    public GameObject Mg11ObstacleL;


    [SerializeField]
    private float Mg11ObstacleSpeed = 5.0f; // �������� �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 1.5f; // ��ֹ� ���� ����
    [SerializeField]
    private int minNumObstaclesToSpawn = 1; // �ּ� ���� ��ֹ� ����
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
                Vector2 spawnPosition = GetRandomSpawnPosition();


                if (spawnPosition.x > 0)
                {
                    GameObject new_Mg11Obstacle = Instantiate(Mg11Obstacle);
                    new_Mg11Obstacle.transform.position = spawnPosition;
                    new_Mg11Obstacle.GetComponent<Mg11Obstacle>().SetSpeed(Mg11ObstacleSpeed); // ��ֹ��� ���ǵ� ����
                    Destroy(new_Mg11Obstacle, 5.0f);
                }
                else
                {
                    GameObject new_Mg11Obstacle = Instantiate(Mg11ObstacleL);
                    new_Mg11Obstacle.transform.position = spawnPosition;
                    new_Mg11Obstacle.GetComponent<Mg11Obstacle>().SetSpeed(Mg11ObstacleSpeed); // ��ֹ��� ���ǵ� ����
                    Destroy(new_Mg11Obstacle, 5.0f);
                }

                // �����ϰ� ��ġ ����


                
            }

            time = Random.Range(0f, 0.5f);
        }
    }


    private Vector2 GetRandomSpawnPosition()
    {
        // ������ ��ġ �ε��� ����
        int randomIndex = Random.Range(0, 3);

        // �̸� ���ǵ� ��ġ�� �迭
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(Random.Range(-10.0f, 10.0f), 9.5f),
        new Vector2(-9.7f, Random.Range(0, 8.3f)),
        new Vector2(9.7f, Random.Range(0, 8.3f))
        };

        // ���õ� ������ ��ġ ��ȯ
        return spawnPositions[randomIndex];
    }

    public void IncreaseSpeed()
    {
        Mg11ObstacleSpeed += 2.0f; // ��ֹ��� ���ǵ� ����
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
}
