using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mg8GroundMoving : MonoBehaviour
{
    public float Groundspeed = 5f; // 땅의 이동 속도
    private float groundWidth; // 땅의 가로 길이

    private void Start()
    {
        

        groundWidth = 4f;
        
    }

    private void Update()
    {
        // 땅을 왼쪽으로 이동시킵니다.
        transform.Translate(Vector2.left * Groundspeed * Time.deltaTime);

        // 땅이 왼쪽으로 벗어나면 오른쪽으로 이동합니다.
        if (transform.position.x < -groundWidth)
        {
            // 땅을 가장 오른쪽에 위치시킵니다.
            float newPositionX = transform.position.x + groundWidth * 2;
            transform.position = new Vector2(newPositionX, transform.position.y);
        }
    }

    public void SetSpeed(float speed)
    {
        Groundspeed = speed;
    }
}
