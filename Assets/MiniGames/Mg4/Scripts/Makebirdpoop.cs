using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Makebirdpoop : MonoBehaviour
{
    public GameObject birdpoop;
    public int minNumPoopsToSpawn = 1; // �ּ� ���� �輳�� ����
    public int maxNumPoopsToSpawn = 1; // �ִ� ���� �輳�� ����

    [SerializeField]
    private float birdpoopSpeed = 5.0f; // �輳���� �ʱ� �ӵ�

    [SerializeField]
    public float minTimeDiff = 1.0f; // �ּ� ���� ����
    [SerializeField]
    public float maxTimeDiff = 2.0f; // �ִ� ���� ����

    float time = 0;
    float timeDiff = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomTimeDiff();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeDiff)
        {
            int numPoopsToSpawn = Random.Range(minNumPoopsToSpawn, maxNumPoopsToSpawn + 1);

            for (int i = 0; i < numPoopsToSpawn; i++)
            {
                GameObject new_birdpoop = Instantiate(birdpoop);
                new_birdpoop.transform.position = new Vector3(10.5f, Random.Range(-1.6f, 3.7f), 0);
                new_birdpoop.GetComponent<birdpoop>().SetSpeed(birdpoopSpeed);
                Destroy(new_birdpoop, 10.0f);
            }

            time = Random.Range(0f, 0.5f);
            SetRandomTimeDiff(); // ���� ���� ������ �������� ����
        }
    }

    void SetRandomTimeDiff()
    {
        timeDiff = Random.Range(minTimeDiff, maxTimeDiff);
    }

    public void IncreaseSpeed()
    {
        minNumPoopsToSpawn += 1;
        maxNumPoopsToSpawn += 1;

        birdpoopSpeed += 2.0f;

        minTimeDiff -= 0.2f;
        maxTimeDiff -= 0.2f;
        if (minTimeDiff < 0.2f)
        {
            minTimeDiff = 0.2f;
        }
        if (maxTimeDiff < 0.2f)
        {
            maxTimeDiff = 0.2f;
        }
    }

    public void DecreaseSpeed()
    {
        minNumPoopsToSpawn -= 1;
        maxNumPoopsToSpawn -= 1;

        birdpoopSpeed -= 2.0f;

        minTimeDiff += 0.2f;
        maxTimeDiff += 0.2f;
        if (minTimeDiff < 0.2f)
        {
            minTimeDiff = 0.2f;
        }
        if (maxTimeDiff < 0.2f)
        {
            maxTimeDiff = 0.2f;
        }
    }



}
