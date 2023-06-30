using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg15Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Mg15Obstacle;

    public GameObject jelly;


    [SerializeField]
    private float Mg15ObstacleSpeed = 1.0f; // 생성물의 초기 중력 상승 속도

    [SerializeField]
    private float Mg15jellySpeed = 1.0f; // 생성물의 초기 중력 상승 속도

    [SerializeField]
    private float time_diff = 2.5f; // 장애물 생성 간격
    [SerializeField]
    private int minNumObstaclesToSpawn = 3; // 최소 생성 장애물 개수
    [SerializeField]
    private int maxNumObstaclesToSpawn = 5; // 최대 생성 장애물 개수

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



                if (Random.value < 0.7f)
                {
                    GameObject new_Mg15Obstacle = Instantiate(Mg15Obstacle);
                    // 랜덤하게 위치 선택
                    Vector2 spawnPosition = GetRandomSpawnPosition();
                    new_Mg15Obstacle.transform.position = spawnPosition;
                    new_Mg15Obstacle.GetComponent<Mg15Obstacle>().SetSpeed(Mg15ObstacleSpeed); // 장애물의 스피드 설정

                    Destroy(new_Mg15Obstacle, 5.0f);
                }
                else
                {
                    GameObject new_jelly = Instantiate(jelly);
                    // 랜덤하게 위치 선택
                    Vector2 spawnPosition = GetRandomSpawnPosition();
                    new_jelly.transform.position = spawnPosition;
                    new_jelly.GetComponent<Mg15jelly>().SetSpeed(Mg15jellySpeed); // 장애물의 스피드 설정

                    Destroy(new_jelly, 5.0f);

                }
            }

            time = 0;
        }
    }


    private Vector2 GetRandomSpawnPosition()
    {
        // 랜덤한 위치 인덱스 선택
        int randomIndex = Random.Range(0, 6);

        // 미리 정의된 위치들 배열
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(1.5f, 0.6f),
        new Vector2(3.5f, 0.6f),
        new Vector2(5.5f, 0.6f),
        new Vector2(-1.5f, 0.6f),
        new Vector2(-3.5f, 0.6f),
        new Vector2(-5.5f, 0.6f)
        };

        // 선택된 랜덤한 위치 반환
        return spawnPositions[randomIndex];
    }

    public void IncreaseSpeed()
    {
        minNumObstaclesToSpawn += 1;
        maxNumObstaclesToSpawn += 1;
        time_diff -= 0.5f; // 장애물의 생성 간격 감소
    }


}
