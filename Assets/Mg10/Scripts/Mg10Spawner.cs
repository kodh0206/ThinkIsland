using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10Spawner : MonoBehaviour
{
    public GameObject Mg10Obstacle;
    public Transform player;

    [SerializeField]
    private float Mg10ObstacleSpeed = 5.0f; // 생성물의 초기 스피드

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
            int numObstaclesToSpawn = Random.Range(minNumObstaclesToSpawn, maxNumObstaclesToSpawn + 1);

            for (int i = 0; i < numObstaclesToSpawn; i++)
            {
                GameObject new_Mg10Obstacle = Instantiate(Mg10Obstacle);

                // 플레이어 위치를 기준으로 좌표 설정
                Vector2 spawnPosition = new Vector2(player.position.x+ Random.Range(-8.0f, 8.0f), player.position.y - 12.5f);
                new_Mg10Obstacle.transform.position = spawnPosition;

                new_Mg10Obstacle.GetComponent<Mg10Obstacle>().SetSpeed(Mg10ObstacleSpeed); // 장애물의 스피드 설정
                Destroy(new_Mg10Obstacle, 5.0f);
            }

            time = 0;
        }
    }

    public void IncreaseSpeed()
    {
        Mg10ObstacleSpeed += 2.0f; // 장애물의 스피드 증가
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
