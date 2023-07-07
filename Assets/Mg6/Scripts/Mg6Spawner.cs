using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg6Spawner : MonoBehaviour
{
    public GameObject Mg6Obstacle;

    [SerializeField]
    private float Mg6ObstacleSpeed = 5.0f; // 젤리의 초기 스피드

    [SerializeField]
    private float time_diff = 1.5f;

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
            GameObject new_Mg6Obstacle = Instantiate(Mg6Obstacle);

            // 좌표를 랜덤하게 선택하여 설정
            Vector2 spawnPosition = Random.value < 0.5f ? new Vector2(9.4f, Random.Range(-1.6f, 6.0f)) : new Vector2(-9.4f, Random.Range(-1.6f, 6.0f));
            new_Mg6Obstacle.transform.position = spawnPosition;

            // 왼쪽에서 생성되면 오른쪽으로 움직이도록 설정
            if (spawnPosition.x < transform.position.x)
            {
                new_Mg6Obstacle.GetComponent<Mg6Obstacle>().obstacledirection = true;
            }
            // 오른쪽에서 생성되면 왼쪽으로 움직이도록 설정
            else
            {
                new_Mg6Obstacle.GetComponent<Mg6Obstacle>().obstacledirection = false;
            }

            new_Mg6Obstacle.GetComponent<Mg6Obstacle>().SetSpeed(Mg6ObstacleSpeed); // 장애물의 스피드 설정
            time = Random.Range(0f,0.3f);
            Destroy(new_Mg6Obstacle, 10.0f);
        }
    }

    public void IncreaseSpeed()
    {
        Mg6ObstacleSpeed += 2.0f; // 장애물의 스피드 증가
        time_diff -= 0.1f; // 장애물의 생선 간격 감소
    }
}
