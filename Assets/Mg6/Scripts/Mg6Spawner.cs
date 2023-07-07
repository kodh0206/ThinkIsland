using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg6Spawner : MonoBehaviour
{
    public GameObject Mg6Obstacle;

    [SerializeField]
    private float Mg6ObstacleSpeed = 5.0f; // ������ �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 1.5f;

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
            GameObject new_Mg6Obstacle = Instantiate(Mg6Obstacle);

            // ��ǥ�� �����ϰ� �����Ͽ� ����
            Vector2 spawnPosition = Random.value < 0.5f ? new Vector2(9.4f, Random.Range(-1.6f, 6.0f)) : new Vector2(-9.4f, Random.Range(-1.6f, 6.0f));
            new_Mg6Obstacle.transform.position = spawnPosition;

            // ���ʿ��� �����Ǹ� ���������� �����̵��� ����
            if (spawnPosition.x < transform.position.x)
            {
                new_Mg6Obstacle.GetComponent<Mg6Obstacle>().obstacledirection = true;
            }
            // �����ʿ��� �����Ǹ� �������� �����̵��� ����
            else
            {
                new_Mg6Obstacle.GetComponent<Mg6Obstacle>().obstacledirection = false;
            }

            new_Mg6Obstacle.GetComponent<Mg6Obstacle>().SetSpeed(Mg6ObstacleSpeed); // ��ֹ��� ���ǵ� ����
            time = Random.Range(0f,0.3f);
            Destroy(new_Mg6Obstacle, 10.0f);
        }
    }

    public void IncreaseSpeed()
    {
        Mg6ObstacleSpeed += 2.0f; // ��ֹ��� ���ǵ� ����
        time_diff -= 0.1f; // ��ֹ��� ���� ���� ����
    }
}
