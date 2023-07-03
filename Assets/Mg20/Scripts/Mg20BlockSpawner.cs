using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20BlockSpawner : MonoBehaviour
{
    public GameObject NomalBlock;
    public GameObject BrokenBlock;

    public GameObject jelly;

    [SerializeField]
    private float BlockSpeed = 2.0f; // 생성물의 초기 스피드

    [SerializeField]
    private float time_diff = 1.5f;

    [SerializeField]
    private int minNumObstaclesToSpawn = 1; // 최소 생성 장애물 세트 개수
    [SerializeField]
    private int maxNumObstaclesToSpawn = 1; // 최대 생성 장애물 세트 개수

    float time = 0;

    int Blockset;

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

                Blockset = Random.Range(0, 4);

                // 랜덤하게 프리팹 선택하여 생성
                if (Blockset == 0)
                {
                    GameObject new_LeftBlock = Instantiate(NomalBlock);
                    GameObject new_RightBlock = Instantiate(NomalBlock);

                    spawnPosition.x += 0.73f;
                    new_LeftBlock.transform.position = spawnPosition;

                    spawnPosition.x -= 1.46f;
                    new_RightBlock.transform.position = spawnPosition;



                    new_LeftBlock.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed); // 장애물의 스피드 설정
                    new_RightBlock.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed); // 장애물의 스피드 설정

                    Destroy(new_LeftBlock, 5.0f);
                    Destroy(new_RightBlock, 5.0f);


                }
                else if (Blockset == 1)
                {
                    GameObject new_LeftBlock = Instantiate(NomalBlock);
                    GameObject new_RightBlock = Instantiate(BrokenBlock);

                    spawnPosition.x += 0.73f;
                    new_LeftBlock.transform.position = spawnPosition;

                    spawnPosition.x -= 1.46f;
                    new_RightBlock.transform.position = spawnPosition;




                    new_LeftBlock.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed); // 장애물의 스피드 설정
                    new_RightBlock.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed); // 장애물의 스피드 설정

                    Destroy(new_LeftBlock, 5.0f);
                    Destroy(new_RightBlock, 5.0f);
                }
                else if(Blockset == 2)
                {
                    GameObject new_LeftBlock = Instantiate(BrokenBlock);
                    GameObject new_RightBlock = Instantiate(NomalBlock);

                    spawnPosition.x += 0.73f;
                    new_LeftBlock.transform.position = spawnPosition;

                    spawnPosition.x -= 1.46f;
                    new_RightBlock.transform.position = spawnPosition;


                    new_LeftBlock.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed); // 장애물의 스피드 설정
                    new_RightBlock.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed); // 장애물의 스피드 설정

                    Destroy(new_LeftBlock, 5.0f);
                    Destroy(new_RightBlock, 5.0f);
                }
                else if (Blockset == 3)
                {
                    GameObject new_LeftBlock = Instantiate(BrokenBlock);
                    GameObject new_RightBlock = Instantiate(BrokenBlock);

                    spawnPosition.x +=0.73f;
                    new_LeftBlock.transform.position = spawnPosition;

                    spawnPosition.x -= 1.46f;
                    new_RightBlock.transform.position = spawnPosition;

                    new_LeftBlock.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed); // 장애물의 스피드 설정
                    new_RightBlock.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed); // 장애물의 스피드 설정

                    Destroy(new_LeftBlock, 5.0f);
                    Destroy(new_RightBlock, 5.0f);
                }


                if (Random.value < 0.4f) //젤리 확률
                {
                    GameObject new_jelly = Instantiate(jelly);
                    spawnPosition.y += 0.7f;
                    spawnPosition.x += 0.73f;
                    new_jelly.transform.position = spawnPosition;
                    new_jelly.GetComponent<Mg20jelly>().SetSpeed(BlockSpeed);
                    Destroy(new_jelly, 7.0f);
                }

                // 좌표를 랜덤하게 선택하여 설정

            }

            time = 0;
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        // 랜덤한 위치 인덱스 선택
        int randomIndex = Random.Range(0, 3);

        // 미리 정의된 위치들 배열
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(0f, -5.5f),
        new Vector2(2.45f, -5.5f),
        new Vector2(-2.2f, -5.5f),
        };

        // 선택된 랜덤한 위치 반환
        return spawnPositions[randomIndex];
    }


    public void IncreaseSpeed()
    {
        BlockSpeed += 2.0f; // 장애물의 스피드 증가
        time_diff -= 0.1f; // 장애물의 생성 간격 감소
    }
}
