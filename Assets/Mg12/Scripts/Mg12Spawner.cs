using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg12Spawner : MonoBehaviour
{
    public GameObject[] Mg12shell; // 0, 1, 2, 3
    

    [SerializeField]
    private float Mg12shellSpeed = 5.0f; // �������� �ʱ� ���ǵ�

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
                int index = Random.Range(0, Mg12shell.Length);
                GameObject new_Mg12shell = Instantiate(Mg12shell[index]);

                // ��ǥ�� �����ϰ� �����Ͽ� ����
                Vector2 spawnPosition = new Vector2(9.4f, Random.Range(-1.6f, 6.0f));
                new_Mg12shell.transform.position = spawnPosition;

                new_Mg12shell.GetComponent<Mg12shell>().SetSpeed(Mg12shellSpeed); // ��ֹ��� ���ǵ� ����
                Destroy(new_Mg12shell, 5.0f);
            }

            time = 0;
        }
    }

    public void IncreaseSpeed()
    {
        Mg12shellSpeed += 2.0f; // ��ֹ��� ���ǵ� ����
        time_diff -= 0.1f; // ��ֹ��� ���� ���� ����
    }
}
