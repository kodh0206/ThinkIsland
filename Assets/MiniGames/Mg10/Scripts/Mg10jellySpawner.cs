using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10jellySpawner : MonoBehaviour
{
    public GameObject jelly;

    public Transform player;

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
            Vector2 spawnPosition = new Vector2(player.position.x + Random.Range(-8.0f, 8.0f), player.position.y - 12.5f); //플레이어를 기준으로 생성
            new_jelly.transform.position = spawnPosition;

            

            new_jelly.GetComponent<Mg10jelly>().SetSpeed(jellySpeed); // 젤리의 스피드 설정
            time = 0;
            Destroy(new_jelly, 5.0f);
        }
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
