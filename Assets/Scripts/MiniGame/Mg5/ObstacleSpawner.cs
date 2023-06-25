using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Mg5obstacle;
    [SerializeField]
    public int minNumMg5obstacleToSpawn = 1; // �ּ� ���� ��ֹ� ����
    [SerializeField]
    public int maxNumMg5obstacleToSpawn = 3; // �ִ� ���� ��ֹ� ����

    [SerializeField]
    private float Mg5obstacleSpeed = 2.0f; // ��ֹ��� �ʱ� �ӵ�

    [SerializeField]
    public float minTimeDiff = 3.0f; // �ּ� ���� ����
    [SerializeField]
    public float maxTimeDiff = 5.0f; // �ִ� ���� ����


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
            int numMg5obstacleToSpawn = Random.Range(minNumMg5obstacleToSpawn, maxNumMg5obstacleToSpawn + 1);

            for (int i = 0; i < numMg5obstacleToSpawn; i++)
            {
                GameObject new_Mg5obstacle = Instantiate(Mg5obstacle);
                float posX = Random.Range(-3f, 7.85f); // x ��ǥ�� �������� ����
                new_Mg5obstacle.transform.position = new Vector3(posX,-5, 0);
                new_Mg5obstacle.GetComponent<Mg5obstacle>().SetSpeed(Mg5obstacleSpeed);

                Destroy(new_Mg5obstacle, 4f );
            }

            time = 0;
            SetRandomTimeDiff(); // ���� ���� ������ �������� ����
        }
    }

    void SetRandomTimeDiff()
    {
        timeDiff = Random.Range(minTimeDiff, maxTimeDiff);
    }

    public void IncreaseSpeed()
    {


        Mg5obstacleSpeed += 1.0f;

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
}
