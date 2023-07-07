using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg17Spawner : MonoBehaviour
{
    public GameObject Mg17shell;

    [SerializeField]
    private float Mg17shellSpeed = 5.0f; // 생성물의 초기 스피드

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
                GameObject new_Mg17shell = Instantiate(Mg17shell);

                // 좌표를 랜덤하게 선택하여 설정
                Vector2 spawnPosition = new Vector2(Random.Range(-3.0f, 3.0f), 5.4f);
                new_Mg17shell.transform.position = spawnPosition;

                new_Mg17shell.GetComponent<Mg17shell>().SetSpeed(Mg17shellSpeed); // 장애물의 스피드 설정
                Destroy(new_Mg17shell, 5.0f);
            }

            time = Random.Range(0f, 1.4f);
        }
    }

    public void IncreaseSpeed()
    {
        Mg17shellSpeed += 2.0f; // 장애물의 스피드 증가
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
