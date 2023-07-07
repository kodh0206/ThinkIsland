using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg17Spawner : MonoBehaviour
{
    public GameObject Mg17shell;

    [SerializeField]
    private float Mg17shellSpeed = 5.0f; // �������� �ʱ� ���ǵ�

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
                GameObject new_Mg17shell = Instantiate(Mg17shell);

                // ��ǥ�� �����ϰ� �����Ͽ� ����
                Vector2 spawnPosition = new Vector2(Random.Range(-3.0f, 3.0f), 5.4f);
                new_Mg17shell.transform.position = spawnPosition;

                new_Mg17shell.GetComponent<Mg17shell>().SetSpeed(Mg17shellSpeed); // ��ֹ��� ���ǵ� ����
                Destroy(new_Mg17shell, 5.0f);
            }

            time = Random.Range(0f, 1.4f);
        }
    }

    public void IncreaseSpeed()
    {
        Mg17shellSpeed += 2.0f; // ��ֹ��� ���ǵ� ����
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
