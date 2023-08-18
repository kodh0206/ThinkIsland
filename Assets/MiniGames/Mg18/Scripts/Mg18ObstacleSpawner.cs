using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg18ObstacleSpawner : MonoBehaviour
{
    public GameObject Mg18Obstacle;

    public GameObject Mg18Seaweed1;

    public int WeedType;

    [SerializeField]
    private float Mg18ObstacleSpeed = 5.0f; // 생성물의 초기 스피드

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



                GameObject new_Mg18Obstacle;


                    new_Mg18Obstacle = Instantiate(Mg18Seaweed1);

                // 좌표를 랜덤하게 선택하여 설정
                
                new_Mg18Obstacle.transform.position = new Vector2(13f, Random.Range(-3f, -2.5f));

                new_Mg18Obstacle.GetComponent<Mg18Seaweed>().SetSpeed(Mg18ObstacleSpeed); // 장애물의 스피드 설정
                Destroy(new_Mg18Obstacle, 5.0f);
            }

            time = 0;
        }
    }


    public void IncreaseSpeed()
    {
        Mg18ObstacleSpeed += 1.0f; // 장애물의 스피드 증가
        ChangeAllSpeed();
    }

    public void DecreaseSpeed()
    {
        Mg18ObstacleSpeed -= 1.0f; // 장애물의 스피드 증가
        ChangeAllSpeed();
    }

    public void ChangeAllSpeed()
    {

        GameObject[] Mg18SeaweedObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var Mg18Seaweeds in Mg18SeaweedObjects)
        {
            if (Mg18Seaweeds != null)
            {
                Mg18Seaweed Mg18SeaweedsComponent = Mg18Seaweeds.GetComponent<Mg18Seaweed>();
                if (Mg18SeaweedsComponent != null)
                {
                    Mg18SeaweedsComponent.SetSpeed(Mg18ObstacleSpeed);
                }
            }
        }

        
    }
}
