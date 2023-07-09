using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg13jellySpawner : MonoBehaviour
{
    public GameObject jelly;



    [SerializeField]
    private float jellySpeed = 5.0f; // 젤리의 초기 스피드

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
            GameObject new_jelly = Instantiate(jelly);

            // 좌표를 랜덤하게 선택하여 설정
            Vector2 spawnPosition = GetRandomSpawnPosition(); //세 곳중 하나 랜덤
            new_jelly.transform.position = spawnPosition;



            new_jelly.GetComponent<Mg13jelly>().SetSpeed(jellySpeed); // 젤리의 스피드 설정
            time = 0;
            Destroy(new_jelly, 5.0f);
        }
    }



    private Vector2 GetRandomSpawnPosition()
    {
        // 랜덤한 위치 인덱스 선택
        int randomIndex = Random.Range(0, 3);

        // 미리 정의된 위치들 배열
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(Random.Range(-10.0f, 10.0f), 9.5f),
        new Vector2(-9.7f, Random.Range(0, 8.3f)),
        new Vector2(9.7f, Random.Range(0, 8.3f)),
        new Vector2(Random.Range(-10.0f, 10.0f),-9.5f)
        };

        // 선택된 랜덤한 위치 반환
        return spawnPositions[randomIndex];
    }

    public void IncreaseSpeed()
    {
        jellySpeed += 2.0f; // 젤리의 스피드 증가
        time_diff -= 0.1f; // 젤리의 생성 간격 감소
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
