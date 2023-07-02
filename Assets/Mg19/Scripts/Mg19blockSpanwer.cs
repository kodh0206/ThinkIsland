using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19blockSpanwer : MonoBehaviour
{
    public GameObject Mg19block;

    public GameObject jelly;

    [SerializeField]
    private float Mg19blockSpeed = 5.0f; // 생성물의 초기 스피드

    [SerializeField]
    private float time_diff = 0.5f; // 장애물 생성 간격
    [SerializeField]
    private int minNumObstaclesToSpawn = 2; // 최소 생성 장애물 개수
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
                GameObject new_Mg19block = Instantiate(Mg19block);
                
                

                // 좌표를 랜덤하게 선택하여 설정
                Vector2 spawnPosition = GetRandomSpawnPosition();
                new_Mg19block.transform.position = spawnPosition;

                new_Mg19block.GetComponent<Mg19block>().SetSpeed(Mg19blockSpeed); // 장애물의 스피드 설정
                Destroy(new_Mg19block, 7.0f);

                if (Random.value < 0.5f)
                {
                    GameObject new_jelly = Instantiate(jelly);
                    spawnPosition.y += 0.7f;
                    new_jelly.transform.position = spawnPosition;
                    new_jelly.GetComponent<Mg19jelly>().SetSpeed(Mg19blockSpeed);
                    Destroy(new_jelly, 7.0f);
                }
            }

            time = 0;
        }
    }


    private Vector2 GetRandomSpawnPosition()
    {
        // 랜덤한 위치 인덱스 선택
        int randomIndex = Random.Range(0, 5);

        // 미리 정의된 위치들 배열
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(0.3f, 7.0f),
        new Vector2(3f, 7.0f),
        new Vector2(-3f, 7.0f),
        new Vector2(6f, 7.0f),
        new Vector2(-6f, 7.0f),
        };

        // 선택된 랜덤한 위치 반환
        return spawnPositions[randomIndex];
    }
    public void IncreaseSpeed()
    {
        Mg19blockSpeed += 2.0f; // 바닥의 스피드 증가

    }
}
