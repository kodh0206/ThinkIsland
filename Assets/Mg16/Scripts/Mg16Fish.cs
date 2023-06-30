using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16Fish : MonoBehaviour
{
    [SerializeField]
    public float fishSpeed = 2.0f;
  
    private float time_diff = 5f;
    float time = 0;

    // y값이 -3.5에서 2.5로 일직선 이동
    public float minY = -3.5f;
    public float maxY = 2.5f;
    // x값 랜덤
    private float randomX;
    private bool isMoving = true;

    private void Start()
    {
        randomX = Random.Range(-9.5f, 9.5f);
        transform.position = new Vector2(randomX, minY);
    }
    void Update()
    {

        // 타임 증가
        time += Time.deltaTime;
        if (time >= time_diff)
        {
            time = 0f; // 타임 초기화
            FishMovement(); // 일정 시간마다 함수 호출
        }

        if (isMoving)
        {
            transform.Translate(Vector2.up * fishSpeed * Time.deltaTime);
            if (transform.position.y > maxY)
            {
                isMoving = false;
                transform.position = new Vector2(transform.position.x, maxY);
            }
        }
    }

    private void FishMovement()
    {
        isMoving = true;
        randomX = Random.Range(-9.5f, 9.5f);
        transform.position = new Vector2(randomX, minY);
    }

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
