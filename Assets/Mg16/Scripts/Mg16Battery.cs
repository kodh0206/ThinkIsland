using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16Battery : MonoBehaviour
{
    public Mg16Manager manager;
    public Mg16Player playerPositionScript;
    public Transform player;
    public float batterySpeed = 2.0f;

    // y값이 -3.5에서 2.5로 일직선 이동
    public float minY = -3.5f;
    public float topY = 3f;
    public bool topArrived = false;
    public float maxY = 1.7f;

    private float time_diff = 5f;
    float time = 0;

    private void Start()
    {
        // Mg16manager 인스턴스 할당
        manager = FindObjectOfType<Mg16Manager>();
        playerPositionScript = FindObjectOfType<Mg16Player>();
    }

    private void Awake()
    {
        transform.position = new Vector2(player.position.x, maxY);
    }

    void Update()
    {

        if (!topArrived && transform.position.y <= topY)
            transform.Translate(Vector2.up * batterySpeed * Time.deltaTime);

        else if (transform.position.y > topY)
        {
            topArrived = true;
            transform.position = new Vector2(playerPositionScript.positionPlayer, topY);
            //Invoke("BatteryDownMove", 0.5f);
        }

        else if (transform.position.y > minY && topArrived)
        {
            transform.Translate(Vector2.down * batterySpeed * Time.deltaTime);
        }

        else if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, minY);
            // 1.5초 후 battery 비활성화 (함수 호출)
            Invoke("BatterySetActiveFalse", 1f);
        }
    }

    public void BatterySetActiveFalse()
    {
        manager.batteryIsArrived = true;
        gameObject.SetActive(false);
        transform.position = new Vector2(playerPositionScript.positionPlayer, maxY);
        topArrived = false;
    }

    public void IncreaseSpeed()
    {
        if (batterySpeed <= 8.0f)
        {
            batterySpeed += 2.0f;
        }
    }

    public void BatteryDownMove()
    {
        Debug.Log("t");
        transform.Translate(Vector2.down * batterySpeed * Time.deltaTime);
    }
}
