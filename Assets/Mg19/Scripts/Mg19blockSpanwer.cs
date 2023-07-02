using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19blockSpanwer : MonoBehaviour
{
    public GameObject Mg19block;

    public GameObject jelly;

    [SerializeField]
    private float Mg19blockSpeed = 5.0f; // �������� �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 0.5f; // ��ֹ� ���� ����
    [SerializeField]
    private int minNumObstaclesToSpawn = 2; // �ּ� ���� ��ֹ� ����
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
                GameObject new_Mg19block = Instantiate(Mg19block);
                
                

                // ��ǥ�� �����ϰ� �����Ͽ� ����
                Vector2 spawnPosition = GetRandomSpawnPosition();
                new_Mg19block.transform.position = spawnPosition;

                new_Mg19block.GetComponent<Mg19block>().SetSpeed(Mg19blockSpeed); // ��ֹ��� ���ǵ� ����
                Destroy(new_Mg19block, 7.0f);

                if (Random.value < 0.5f)
                {
                    GameObject new_jelly = Instantiate(jelly);
                    spawnPosition.y += 0.7f;
                    new_jelly.transform.position = spawnPosition;
                    new_jelly.GetComponent<Mg19jelly>().SetSpeed(Mg19blockSpeed);
                    Destroy(new_jelly, 7.0f);
                }
            }

            time = 0;
        }
    }


    private Vector2 GetRandomSpawnPosition()
    {
        // ������ ��ġ �ε��� ����
        int randomIndex = Random.Range(0, 5);

        // �̸� ���ǵ� ��ġ�� �迭
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(0.3f, 7.0f),
        new Vector2(3f, 7.0f),
        new Vector2(-3f, 7.0f),
        new Vector2(6f, 7.0f),
        new Vector2(-6f, 7.0f),
        };

        // ���õ� ������ ��ġ ��ȯ
        return spawnPositions[randomIndex];
    }
    public void IncreaseSpeed()
    {
        Mg19blockSpeed += 2.0f; // �ٴ��� ���ǵ� ����

    }
}
