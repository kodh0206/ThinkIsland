using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Mg5Obstacle;
    [SerializeField]
    public int minNumMg5ObstacleToSpawn = 3; // �ּ� ���� ��ֹ� ����
    [SerializeField]
    public int maxNumMg5ObstacleToSpawn = 5; // �ִ� ���� ��ֹ� ����

    [SerializeField]
    private float Mg5ObstacleSpeed = 4.0f; // ��ֹ��� �ʱ� �ӵ�

    [SerializeField]
    public float minTimeDiff = 2.0f; // �ּ� ���� ����
    [SerializeField]
    public float maxTimeDiff = 3.0f; // �ִ� ���� ����


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
            int numMg5obstacleToSpawn = Random.Range(minNumMg5ObstacleToSpawn, maxNumMg5ObstacleToSpawn + 1);

            for (int i = 0; i < numMg5obstacleToSpawn; i++)
            {
                GameObject new_Mg5obstacle = Instantiate(Mg5Obstacle);
                float posX = Random.Range(-5f, 7.85f); // x ��ǥ�� �������� ����
                new_Mg5obstacle.transform.position = new Vector3(posX,-5f, 0);
                new_Mg5obstacle.GetComponent<Mg5Obstacle>().SetSpeed(Mg5ObstacleSpeed);

                Destroy(new_Mg5obstacle, 7f );
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


        Mg5ObstacleSpeed += 1.0f;

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
