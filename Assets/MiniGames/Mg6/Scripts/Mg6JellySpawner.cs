using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg6JellySpawner : MonoBehaviour
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
            Vector2 spawnPosition = Random.value < 0.5f ? new Vector2(9.4f, Random.Range(-1.6f, 6.0f)) : new Vector2(-9.4f, Random.Range(-1.6f, 6.0f));
            new_jelly.transform.position = spawnPosition;

            // 왼쪽에서 생성되면 오른쪽으로 움직이도록 설정
            if (spawnPosition.x < transform.position.x)
            {
                new_jelly.GetComponent<Mg6jelly>().jellydirection = true;
            }
            // 오른쪽에서 생성되면 왼쪽으로 움직이도록 설정
            else
            {
                new_jelly.GetComponent<Mg6jelly>().jellydirection = false;
            }

            new_jelly.GetComponent<Mg6jelly>().SetSpeed(jellySpeed); // 젤리의 스피드 설정
            time = 0;
            Destroy(new_jelly, 10.0f);
        }
    }

    public void IncreaseSpeed()
    {
        jellySpeed += 1.0f; // 젤리의 스피드 증가
        time_diff -= 0.2f;
    }

    public void DecreaseSpeed()
    {
        jellySpeed -= 1.0f; // 젤리의 스피드 증가
        time_diff += 0.2f;
    }

}
