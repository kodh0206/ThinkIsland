using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16BatteryMove : MonoBehaviour
{
    public GameObject battery;

    public Transform player;

    [SerializeField]
    private float batterySpeed = 2.0f;

    [SerializeField]
    private float time_diff = 5f;

    float time = 0;

    void Start()
    {

    }

    void Update()
    {
        // 밑으로 가라앉는 배터리 움직임 구현
        time += Time.deltaTime;
        if (time > time_diff)
        {
            GameObject new_Battery = Instantiate(battery);
            // 플레이어 위치 == 시작점
            Vector2 spawnPosition = new Vector2(player.position.x , player.position.y);
            new_Battery.transform.position = spawnPosition;

            new_Battery.GetComponent<Mg16Battery>().SetSpeed(batterySpeed);
            time = 0;
            //Destroy(new_Battery, 5.0f);
        }
    }

    public void IncreaseSpeed()
    {
        batterySpeed += 2.0f;
        time_diff -= 0.1f;
    }

/*
    public void GetHit()
    {
        StartCoroutine(DisableSpawning());
    }

    private IEnumerator DisableSpawning()
    {
        time_diff = Mathf.Infinity;

        yield return new WaitForSeconds(2f);

        time_diff = 1.5f;
    }*/
}
