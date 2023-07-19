using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg8jellySpawner : MonoBehaviour
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
            Vector2 spawnPosition =new Vector2(12.4f, Random.Range(-1.6f, 4.0f));
            new_jelly.transform.position = spawnPosition;

            // ���ʿ��� �����Ǹ� ���������� �����̵��� ����

            new_jelly.GetComponent<Mg8jelly>().SetSpeed(jellySpeed); // ������ ���ǵ� ����
            time = 0;
            Destroy(new_jelly, 5.0f);
        }
    }

    public void IncreaseSpeed()
    {
        jellySpeed += 1.0f; // ������ ���ǵ� ����
        time_diff -= 0.1f; // ������ ���� ���� ����
        
    }

    public void DecreaseSpeed()
    {
        jellySpeed -= 1.0f; // ������ ���ǵ� ����
        time_diff += 0.1f; // ������ ���� ���� ����

        
    }

    
}
