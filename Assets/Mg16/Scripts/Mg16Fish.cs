using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16Fish : MonoBehaviour
{

    public Mg16Manager manager;
    [SerializeField]
    public float fishSpeed = 2.0f;
  
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
        transform.Translate(Vector2.up * fishSpeed * Time.deltaTime);
        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
            // 1.5초 후 battery 비활성화 (함수 호출)
            Invoke("FishSetActiveFalse", 1.5f);
        }
    }

    public void FishSetActiveFalse()
    {
        manager.objectIsArrived = true;
        gameObject.SetActive(false);
        randomX = Random.Range(-9.5f, 9.5f);
        transform.position = new Vector2(randomX, minY);
    }

/*
    private void FishMovement()
    {
        isMoving = true;
        randomX = Random.Range(-9.5f, 9.5f);
        transform.position = new Vector2(randomX, minY);
    }*/

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger 발생");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌");
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }*/
}
