using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg11Spawner : MonoBehaviour
{
    public GameObject Mg11Obstacle;
    public GameObject Mg11ObstacleL;


    [SerializeField]
    private float Mg11ObstacleSpeed = 5.0f; // 생성물의 초기 스피드

    [SerializeField]
    private float time_diff = 1.5f; // 장애물 생성 간격
    [SerializeField]
    private int minNumObstaclesToSpawn = 1; // 최소 생성 장애물 개수
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
                Vector2 spawnPosition = GetRandomSpawnPosition();


                if (spawnPosition.x > 0)
                {
                    GameObject new_Mg11Obstacle = Instantiate(Mg11Obstacle);
                    new_Mg11Obstacle.transform.position = spawnPosition;
                    new_Mg11Obstacle.GetComponent<Mg11Obstacle>().SetSpeed(Mg11ObstacleSpeed); // 장애물의 스피드 설정
                    Destroy(new_Mg11Obstacle, 5.0f);
                }
                else
                {
                    GameObject new_Mg11Obstacle = Instantiate(Mg11ObstacleL);
                    new_Mg11Obstacle.transform.position = spawnPosition;
                    new_Mg11Obstacle.GetComponent<Mg11Obstacle>().SetSpeed(Mg11ObstacleSpeed); // 장애물의 스피드 설정
                    Destroy(new_Mg11Obstacle, 5.0f);
                }

                // 랜덤하게 위치 선택


                
            }

            time = Random.Range(0f, 0.5f);
        }
    }


    private Vector2 GetRandomSpawnPosition()
    {
        // 랜덤한 위치 인덱스 선택
        int randomIndex = Random.Range(0, 3);

        // 미리 정의된 위치들 배열
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(Random.Range(-10.0f, 10.0f), 9.5f),
        new Vector2(-9.7f, Random.Range(0, 8.3f)),
        new Vector2(9.7f, Random.Range(0, 8.3f))
        };

        // 선택된 랜덤한 위치 반환
        return spawnPositions[randomIndex];
    }

    public void IncreaseSpeed()
    {
        Mg11ObstacleSpeed += 2.0f; // 장애물의 스피드 증가
        time_diff -= 0.1f; // 장애물의 생성 간격 감소
    }

    public void GetHit()
    {
        StartCoroutine(DisableSpawning());
    }

    private IEnumerator DisableSpawning()
    {
        // 생성 멈춤
        time_diff = Mathf.Infinity;

        // 대기 시간
        yield return new WaitForSeconds(2f);

        // 생성 재개
        time_diff = 1.5f;
    }
}
