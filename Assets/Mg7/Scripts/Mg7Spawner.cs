using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg7Spawner : MonoBehaviour
{
    public GameObject Mg7Obstacle1;
    public GameObject Mg7Obstacle2;

    [SerializeField]
    private float Mg7ObstacleSpeed = 5.0f; // 생성물의 초기 스피드

    [SerializeField]
    private float time_diff = 1.5f;

    [SerializeField]
    private int minNumObstaclesToSpawn = 1; // 최소 생성 장애물 개수
    [SerializeField]
    private int maxNumObstaclesToSpawn = 3; // 최대 생성 장애물 개수

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
                GameObject new_Mg7Obstacle;

                // 랜덤하게 프리팹 선택하여 생성
                if (Random.Range(0, 2) == 0)
                {
                    new_Mg7Obstacle = Instantiate(Mg7Obstacle1);
                }
                else
                {
                    new_Mg7Obstacle = Instantiate(Mg7Obstacle2);
                }

                // 좌표를 랜덤하게 선택하여 설정
                Vector2 spawnPosition = new Vector2(Random.Range(-8.6f, 8.0f), 6.4f);
                new_Mg7Obstacle.transform.position = spawnPosition;

                new_Mg7Obstacle.GetComponent<Mg7Obstacle>().SetSpeed(Mg7ObstacleSpeed); // 장애물의 스피드 설정
                Destroy(new_Mg7Obstacle, 5.0f);
            }

            time = 0;
        }
    }

    public void IncreaseSpeed()
    {
        Mg7ObstacleSpeed += 2.0f; // 장애물의 스피드 증가
        time_diff -= 0.1f; // 장애물의 생성 간격 감소
        minNumObstaclesToSpawn += 1;
        maxNumObstaclesToSpawn += 1;
    }
}
