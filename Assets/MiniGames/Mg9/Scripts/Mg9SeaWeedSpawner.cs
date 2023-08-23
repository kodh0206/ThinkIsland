using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg9SeaWeedSpawner : MonoBehaviour
{
    public GameObject Mg9Seaweed1;

    public int WeedType;

    [SerializeField]
    private float Mg9ObstacleSpeed = 5.0f; // �������� �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 1.5f; // ��ֹ� ���� ����
    [SerializeField]
    private int minNumObstaclesToSpawn = 0; // �ּ� ���� ��ֹ� ����
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


                GameObject new_Mg9OSeaweed;


                new_Mg9OSeaweed = Instantiate(Mg9Seaweed1);

                // ��ǥ�� �����ϰ� �����Ͽ� ����

                new_Mg9OSeaweed.transform.position = new Vector2(13f, Random.Range(-3f, -2.5f));

                new_Mg9OSeaweed.GetComponent<Mg18Seaweed>().SetSpeed(Mg9ObstacleSpeed); // ��ֹ��� ���ǵ� ����
                Destroy(new_Mg9OSeaweed, 5.0f);
            }

            time = 0;
        }
    }


    public void IncreaseSpeed()
    {
        Mg9ObstacleSpeed += 1.0f; // ��ֹ��� ���ǵ� ����
        ChangeAllSpeed();
    }

    public void DecreaseSpeed()
    {
        Mg9ObstacleSpeed -= 1.0f; // ��ֹ��� ���ǵ� ����
        ChangeAllSpeed();
    }

    public void ChangeAllSpeed()
    {

        GameObject[] Mg9SeaweedObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var Mg9Seaweeds in Mg9SeaweedObjects)
        {
            if (Mg9Seaweeds != null)
            {
                Mg18Seaweed Mg18SeaweedsComponent = Mg9Seaweeds.GetComponent<Mg18Seaweed>();
                if (Mg18SeaweedsComponent != null)
                {
                    Mg18SeaweedsComponent.SetSpeed(Mg9ObstacleSpeed);
                }
            }
        }


    }
}
