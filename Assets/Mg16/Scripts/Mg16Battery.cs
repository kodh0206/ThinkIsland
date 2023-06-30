using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16Battery : MonoBehaviour
{
    public Transform player;
    public float batterySpeed = 2.0f;

    // y값이 -3.5에서 2.5로 일직선 이동
    public float minY = -3.5f;
    public float maxY = 2.5f;

    private float time_diff = 5f;
    float time = 0;
    private bool isMoving = true;

    private void Start()
    {
        transform.position = new Vector2(player.position.x, maxY);
    }
    void Update()
    {
        // 타임 증가
        time += Time.deltaTime;
        if (time >= time_diff)
        {
            time = 0f; // 타임 초기화
            BatteryMovement(); // 일정 시간마다 함수 호출
        }

        if (isMoving)
        {
            transform.Translate(Vector2.down * batterySpeed * Time.deltaTime);

            if (transform.position.y < minY)
            {
                transform.position = new Vector2(transform.position.x, minY);
            }
        }
    }

    private void BatteryMovement()
    {
        isMoving = true;
        transform.position = new Vector2(player.position.x, maxY);
    }

    public void SetSpeed(float speed)
    {
        batterySpeed = speed;
    }
}
