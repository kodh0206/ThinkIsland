using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg18GroundSpawner : MonoBehaviour
{
    public GameObject Mg18movigGround;

    [SerializeField]
    private float Mg18movigGroundSpeed = 5.0f; // �������� �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 1.0f; // ���� ���� ����
    [SerializeField]
    private int minNumObstaclesToSpawn = 1; // �ּ� ���� ���� ����
    [SerializeField]
    private int maxNumObstaclesToSpawn = 1; // �ִ� ���� ���� ����

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
                GameObject new_Mg18movigGround = Instantiate(Mg18movigGround);

                // ��ǥ�� �����ϰ� �����Ͽ� ����
                Vector2 spawnPosition = GetRandomSpawnPosition();
                new_Mg18movigGround.transform.position = spawnPosition;

                new_Mg18movigGround.GetComponent<Mg18movingGround>().SetSpeed(Mg18movigGroundSpeed); // ��ֹ��� ���ǵ� ����
                Destroy(new_Mg18movigGround, 5.0f);
            }

            time = 0;
        }
    }


    private Vector2 GetRandomSpawnPosition()
    {
        // ������ ��ġ �ε��� ����
        int randomIndex = Random.Range(0, 3);

        // �̸� ���ǵ� ��ġ�� �迭
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(13f, -0.5f),
        new Vector2(13f, 1f),
        new Vector2(13f, 2.5f),
        };

        // ���õ� ������ ��ġ ��ȯ
        return spawnPositions[randomIndex];
    }
    public void IncreaseSpeed()
    {
        Mg18movigGroundSpeed += 2.0f; // �ٴ��� ���ǵ� ����
        
    }
}
