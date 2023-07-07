using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg9Spawner : MonoBehaviour
{
    public GameObject Mg9Obstacle;

    [SerializeField]
    private float Mg9ObstacleSpeed = 5.0f; // 생성물의 초기 스피드

    [SerializeField]
    private float time_diff = 1.5f; // 장애물 생성 간격
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

            
            GameObject new_Mg9Obstacle = Instantiate(Mg9Obstacle);

                // 좌표를 랜덤하게 선택하여 설정
                Vector2 spawnPosition = new Vector2(9.4f, Random.Range(-1.6f, 6.0f));
            new_Mg9Obstacle.transform.position = spawnPosition;

            new_Mg9Obstacle.GetComponent<Mg9Obstacle>().SetSpeed(Mg9ObstacleSpeed); // 장애물의 스피드 설정
            Destroy(new_Mg9Obstacle, 5.0f);

            time = Random.Range(0f, 0.3f);
        }
    }

    public void IncreaseSpeed()
    {
        Mg9ObstacleSpeed += 2.0f; // 장애물의 스피드 증가
        time_diff -= 0.1f; // 장애물의 생성 간격 감소
    }



}
