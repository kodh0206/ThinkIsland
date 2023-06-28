using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg12Spawner : MonoBehaviour
{
    public GameObject Mg12shell;

    [SerializeField]
    private float Mg12shellSpeed = 5.0f; // 생성물의 초기 스피드

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
                GameObject new_Mg12shell = Instantiate(Mg12shell);

                // 좌표를 랜덤하게 선택하여 설정
                Vector2 spawnPosition = new Vector2(9.4f, Random.Range(-1.6f, 6.0f));
                new_Mg12shell.transform.position = spawnPosition;

                new_Mg12shell.GetComponent<Mg12shell>().SetSpeed(Mg12shellSpeed); // 장애물의 스피드 설정
                Destroy(new_Mg12shell, 5.0f);
            }

            time = 0;
        }
    }

    public void IncreaseSpeed()
    {
        Mg12shellSpeed += 2.0f; // 장애물의 스피드 증가
        time_diff -= 0.1f; // 장애물의 생성 간격 감소
    }
}
