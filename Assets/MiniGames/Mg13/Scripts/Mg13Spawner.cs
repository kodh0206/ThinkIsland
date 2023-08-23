using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg13Spawner : MonoBehaviour
{
    public GameObject Mg13Obstacle;


    [SerializeField]
    private float Mg13ObstacleSpeed = 4.0f; // �������� �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 1.5f; // ��ֹ� ���� ����
    [SerializeField]
    private int minNumObstaclesToSpawn = 1; // �ּ� ���� ��ֹ� ����
    [SerializeField]
    private int maxNumObstaclesToSpawn = 1; // �ִ� ���� ��ֹ� ����

    Vector2[] spawnPositions;
       

    float time = 0;
    void Awake()
    {
        spawnPositions = new Vector2[]
        {
            new Vector2(Random.Range(-10.0f, 10.0f), 9.5f),
            new Vector2(-9.7f, Random.Range(0, 8.3f)),
            new Vector2(9.7f, Random.Range(0, 8.3f)),
            new Vector2(Random.Range(-10.0f, 10.0f), -9.5f),
        };
    }
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
                GameObject new_Mg13Obstacle = Instantiate(Mg13Obstacle);

                // �����ϰ� ��ġ ����
                Vector2 spawnPosition = GetRandomSpawnPosition();
                new_Mg13Obstacle.transform.position = spawnPosition;

                new_Mg13Obstacle.GetComponent<Mg13Obstacle>().SetSpeed(Mg13ObstacleSpeed); // ��ֹ��� ���ǵ� ����
                Destroy(new_Mg13Obstacle, 5.0f);
            }

            time = Random.Range(0f, 0.5f);
        }
    }


    private Vector2 GetRandomSpawnPosition()
    {
        // ������ ��ġ �ε��� ����
        int randomIndex = Random.Range(0, 4);

        // �̸� ���ǵ� ��ġ�� �迭
        

        // ���õ� ������ ��ġ ��ȯ
        return spawnPositions[randomIndex];
    }

    public void IncreaseSpeed()
    {
        Mg13ObstacleSpeed += 1.0f; // ��ֹ��� ���ǵ� ����
        time_diff -= 0.1f; // ��ֹ��� ���� ���� ����
    }

    public void DecreaseSpeed()
    {
        Mg13ObstacleSpeed -= 1.0f; // ��ֹ��� ���ǵ� ����
        time_diff += 0.1f; // ��ֹ��� ���� ���� ����
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
