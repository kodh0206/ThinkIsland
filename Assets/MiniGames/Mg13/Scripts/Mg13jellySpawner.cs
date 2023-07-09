using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg13jellySpawner : MonoBehaviour
{
    public GameObject jelly;



    [SerializeField]
    private float jellySpeed = 5.0f; // ������ �ʱ� ���ǵ�

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
            GameObject new_jelly = Instantiate(jelly);

            // ��ǥ�� �����ϰ� �����Ͽ� ����
            Vector2 spawnPosition = GetRandomSpawnPosition(); //�� ���� �ϳ� ����
            new_jelly.transform.position = spawnPosition;



            new_jelly.GetComponent<Mg13jelly>().SetSpeed(jellySpeed); // ������ ���ǵ� ����
            time = 0;
            Destroy(new_jelly, 5.0f);
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
        new Vector2(9.7f, Random.Range(0, 8.3f)),
        new Vector2(Random.Range(-10.0f, 10.0f),-9.5f)
        };

        // ���õ� ������ ��ġ ��ȯ
        return spawnPositions[randomIndex];
    }

    public void IncreaseSpeed()
    {
        jellySpeed += 2.0f; // ������ ���ǵ� ����
        time_diff -= 0.1f; // ������ ���� ���� ����
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
