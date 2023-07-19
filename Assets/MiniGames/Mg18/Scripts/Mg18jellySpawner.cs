using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg18jellySpawner : MonoBehaviour
{
    public GameObject jelly;

    [SerializeField]
    private float jellySpeed = 5.0f; // 생성물의 초기 스피드

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
                GameObject new_Mg18jelly = Instantiate(jelly);

                // 좌표를 랜덤하게 선택하여 설정
                Vector2 spawnPosition = GetRandomSpawnPosition();
                new_Mg18jelly.transform.position = spawnPosition;

                new_Mg18jelly.GetComponent<Mg18jelly>().SetSpeed(jellySpeed); // 장애물의 스피드 설정
                Destroy(new_Mg18jelly, 5.0f);
            }

            time = Random.Range(0.1f,0.5f);
        }
    }


    private Vector2 GetRandomSpawnPosition()
    {
        // 랜덤한 위치 인덱스 선택
        int randomIndex = Random.Range(0, 1);

        // 미리 정의된 위치들 배열
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(13f, Random.Range(-4f, -1f)),
       // new Vector2(13f, 0f),
        //new Vector2(13f, 1.5f),
        //new Vector2(13f, 3f),
        };

        // 선택된 랜덤한 위치 반환
        return spawnPositions[randomIndex];
    }
    public void IncreaseSpeed()
    {
        jellySpeed += 1.0f; // 장애물의 스피드 증가
    }

    public void DecreaseSpeed()
    {
        jellySpeed -= 1.0f; // 장애물의 스피드 증가
    }
}
