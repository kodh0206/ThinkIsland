using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10Spawner : MonoBehaviour
{
    public GameObject Mg10Obstacle;
    public Transform player;

    [SerializeField]
    private float Mg10ObstacleSpeed = 5.0f; // �������� �ʱ� ���ǵ�

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
                GameObject new_Mg10Obstacle = Instantiate(Mg10Obstacle);

                // �÷��̾� ��ġ�� �������� ��ǥ ����
                Vector2 spawnPosition = new Vector2(player.position.x+ Random.Range(-8.0f, 8.0f), player.position.y - 12.5f);
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
}
