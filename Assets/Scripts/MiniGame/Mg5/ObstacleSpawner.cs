using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Mg5Obstacle;
    [SerializeField]
    public int minNumMg5ObstacleToSpawn = 3; // 최소 생성 장애물 개수
    [SerializeField]
    public int maxNumMg5ObstacleToSpawn = 5; // 최대 생성 장애물 개수

    [SerializeField]
    private float Mg5ObstacleSpeed = 4.0f; // 장애물의 초기 속도

    [SerializeField]
    public float minTimeDiff = 2.0f; // 최소 생성 간격
    [SerializeField]
    public float maxTimeDiff = 3.0f; // 최대 생성 간격


    float time = 0;
    float timeDiff = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomTimeDiff();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeDiff)
        {
            int numMg5obstacleToSpawn = Random.Range(minNumMg5ObstacleToSpawn, maxNumMg5ObstacleToSpawn + 1);

            for (int i = 0; i < numMg5obstacleToSpawn; i++)
            {
                GameObject new_Mg5obstacle = Instantiate(Mg5Obstacle);
                float posX = Random.Range(-5f, 7.85f); // x 좌표를 랜덤으로 설정
                new_Mg5obstacle.transform.position = new Vector3(posX,-5f, 0);
                new_Mg5obstacle.GetComponent<Mg5Obstacle>().SetSpeed(Mg5ObstacleSpeed);

                Destroy(new_Mg5obstacle, 7f );
            }

            time = 0;
            SetRandomTimeDiff(); // 다음 생성 간격을 랜덤으로 설정
        }
    }

    void SetRandomTimeDiff()
    {
        timeDiff = Random.Range(minTimeDiff, maxTimeDiff);
    }

    public void IncreaseSpeed()
    {


        Mg5ObstacleSpeed += 1.0f;

        minTimeDiff -= 0.2f;
        maxTimeDiff -= 0.2f;
        if (minTimeDiff < 0.2f)
        {
            minTimeDiff = 0.2f;
        }
        if (maxTimeDiff < 0.2f)
        {
            maxTimeDiff = 0.2f;
        }
    }
}
