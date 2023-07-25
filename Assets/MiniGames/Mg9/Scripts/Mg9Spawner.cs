using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg9Spawner : MonoBehaviour
{
    public GameObject Mg9Obstacle;

    [SerializeField]
    private float Mg9ObstacleSpeed = 5.0f; // �������� �ʱ� ���ǵ�

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

            
            GameObject new_Mg9Obstacle = Instantiate(Mg9Obstacle);

                // ��ǥ�� �����ϰ� �����Ͽ� ����
                Vector2 spawnPosition = new Vector2(9.4f, Random.Range(-3.4f, 3.3f));
            new_Mg9Obstacle.transform.position = spawnPosition;

            new_Mg9Obstacle.GetComponent<Mg9Obstacle>().SetSpeed(Mg9ObstacleSpeed); // ��ֹ��� ���ǵ� ����
            Destroy(new_Mg9Obstacle, 5.0f);

            time = Random.Range(0f, 0.3f);
        }
    }

    public void IncreaseSpeed()
    {
        Mg9ObstacleSpeed += 2.0f; // ��ֹ��� ���ǵ� ����
        time_diff -= 0.1f; // ��ֹ��� ���� ���� ����
    }

    public void DecreaseSpeed()
    {
        Mg9ObstacleSpeed -= 2.0f; // ��ֹ��� ���ǵ� ����
        time_diff += 0.1f; // ��ֹ��� ���� ���� ����
    }

}
