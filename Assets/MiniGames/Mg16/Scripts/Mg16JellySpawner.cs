using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16JellySpawner : MonoBehaviour
{

    Mg16Jelly mg16Jelly;
    public GameObject jelly;
    public Transform player;

    [SerializeField]

    // fish & jelly가 올라왔다 내려옴(3초 소요) + 정지 (1초 소요)
    public float time_diff = 4f;

    float time = 0;

    // x값 랜덤
    private float randomX;

    void Start()
    {
        mg16Jelly = GetComponent<Mg16Jelly>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > time_diff)
        {
            GameObject new_jelly = Instantiate(jelly);

            Vector2 spawnPosition = new Vector2(player.position.x, -3.5f);//mg16Fish.startY, player.position.y);
            new_jelly.transform.position = spawnPosition; // 위치 수정
            new_jelly.GetComponent<Mg16Jelly>();
            time = 0;
            if (gameObject != null)
            {
                Destroy(new_jelly, 4f);
            }
        }
    }

    public void SpeedTime()
    {
        if (time_diff >= 3.1f)
        {
            time_diff -= 0.3f;
        }
    }
}
