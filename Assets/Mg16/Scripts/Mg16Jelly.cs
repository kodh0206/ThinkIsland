using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16Jelly : MonoBehaviour
{
    public Mg16Manager manager;

    [SerializeField]
    public float jellySpeed = 2.0f;
  
    private float time_diff = 5f;
    float time = 0;

    // y값이 -3.5에서 2.5로 일직선 이동
    public float minY = -3.5f;
    public float maxY = 2.5f;
    // x값 랜덤
    private float randomX;

    private void Start()
    {
        // Mg16manager 인스턴스 할당
        manager = FindObjectOfType<Mg16Manager>();
    }

    private void Awake()
    {
        randomX = Random.Range(-9.5f, 9.5f);
        transform.position = new Vector2(randomX, minY);
    }

    void Update()
    {
        transform.Translate(Vector2.up * jellySpeed * Time.deltaTime);
        manager.jellyIsMoving = true;
        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
            manager.jellyIsMoving = false;
            // 1.5초 후 jelly 비활성화 (함수 호출)
            Invoke("JellySetActiveFalse", 1.5f);
        }
    }

    public void JellySetActiveFalse()
    {
        gameObject.SetActive(false);
        randomX = Random.Range(-9.5f, 9.5f);
        transform.position = new Vector2(randomX, minY);
        manager.jellyIsArrived = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MiniGameManager.Instance.AddJelly();
            manager.AddScore();
            gameObject.SetActive(false);
        }
    }

    public void IncreaseSpeed()
    {
        if (jellySpeed <= 10.0f)
        {
            jellySpeed += 2.0f;
        }
        else
        {
            jellySpeed = 10.0f;
        }
    }
}
