using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16Battery : MonoBehaviour
{
    public Mg16Manager manager;
    public Transform player;
    public float batterySpeed = 2.0f;

    // 플레이어 위치 확인
    public float positionPlayer;

    // y값이 -3.5에서 2.5로 일직선 이동
    public float minY = -3.5f;
    public float maxY = 2.5f;

    private float time_diff = 5f;
    float time = 0;

    private void Start()
    {
        // Mg16manager 인스턴스 할당
        manager = FindObjectOfType<Mg16Manager>();
    }

    private void Awake()
    {
        transform.position = new Vector2(player.position.x, maxY);
    }

    void Update()
    {

        // 플레이어 위치 확인
        positionPlayer = player.position.x;
        transform.Translate(Vector2.down * batterySpeed * Time.deltaTime);

        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, minY);
            // 1.5초 후 battery 비활성화 (함수 호출)
            Invoke("BatterySetActiveFalse", 1.5f);
        }
    }

    public void BatterySetActiveFalse()
    {
        manager.batteryIsArrived = true;
        gameObject.SetActive(false);
        transform.position = new Vector2(player.position.x, maxY);
    }
    public void SetSpeed(float speed)
    {
        batterySpeed = speed;
    }
}
