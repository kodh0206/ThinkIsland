using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg8Spawner : MonoBehaviour
{
    public GameObject Mg8Obstacle;

    [SerializeField]
    private float Mg8ObstacleSpeed = 5.0f; // �������� �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 1.5f; // ��ֹ� ���� ����
    [SerializeField]
    private int minNumObstaclesToSpawn = 1; // �ּ� ���� ��ֹ� ����
    [SerializeField]
    private int maxNumObstaclesToSpawn = 3; // �ִ� ���� ��ֹ� ����

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
                GameObject new_Mg8Obstacle = Instantiate(Mg8Obstacle);

                // ��ǥ�� �����ϰ� �����Ͽ� ����
                Vector2 spawnPosition = new Vector2(9.4f, Random.Range(-1.6f, 6.0f));
                new_Mg8Obstacle.transform.position = spawnPosition;

                new_Mg8Obstacle.GetComponent<Mg8Obstacle>().SetSpeed(Mg8ObstacleSpeed); // ��ֹ��� ���ǵ� ����
                Destroy(new_Mg8Obstacle, 5.0f);
            }

            time = 0;
        }
    }

    public void IncreaseSpeed()
    {
        Mg8ObstacleSpeed += 2.0f; // ��ֹ��� ���ǵ� ����
        time_diff -= 0.1f; // ��ֹ��� ���� ���� ����
    }
}
