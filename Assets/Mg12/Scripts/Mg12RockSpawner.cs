using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg12RockSpawner : MonoBehaviour
{
    public GameObject Rock;

    public Transform player;

    [SerializeField]
    private float RockSpeed = 5.0f; // 바위의 초기 스피드

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
            GameObject new_Rock = Instantiate(Rock);

            // 좌표를 랜덤하게 선택하여 설정
            Vector2 spawnPosition = new Vector2(player.position.x , player.position.y); //플레이어를 기준으로 생성
            new_Rock.transform.position = spawnPosition;



            new_Rock.GetComponent<Mg12Rock>().SetSpeed(RockSpeed); // 젤리의 스피드 설정
            time = 0;
            Destroy(new_Rock, 5.0f);
        }
    }

    public void IncreaseSpeed()
    {
        RockSpeed += 2.0f; // 바위의 스피드 증가
        time_diff -= 0.1f; // 바위의 생성 간격 감소
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
