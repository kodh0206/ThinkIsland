using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg9SeaWeedSpawner : MonoBehaviour
{
    public GameObject Mg9Seaweed1;

    public int WeedType;

    [SerializeField]
    private float Mg9ObstacleSpeed = 5.0f; // 생성물의 초기 스피드

    [SerializeField]
    private float time_diff = 1.5f; // 장애물 생성 간격
    [SerializeField]
    private int minNumObstaclesToSpawn = 0; // 최소 생성 장애물 개수
    [SerializeField]
    private int maxNumObstaclesToSpawn = 1; // 최대 생성 장애물 개수

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

                // 좌표를 랜덤하게 선택하여 설정

                new_Mg9OSeaweed.transform.position = new Vector2(13f, Random.Range(-3f, -2.5f));

                new_Mg9OSeaweed.GetComponent<Mg18Seaweed>().SetSpeed(Mg9ObstacleSpeed); // 장애물의 스피드 설정
                Destroy(new_Mg9OSeaweed, 5.0f);
            }

            time = 0;
        }
    }


    public void IncreaseSpeed()
    {
        Mg9ObstacleSpeed += 1.0f; // 장애물의 스피드 증가
        ChangeAllSpeed();
    }

    public void DecreaseSpeed()
    {
        Mg9ObstacleSpeed -= 1.0f; // 장애물의 스피드 증가
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
