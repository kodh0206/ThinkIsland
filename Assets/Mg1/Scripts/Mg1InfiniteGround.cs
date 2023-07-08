using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1InfiniteGround : MonoBehaviour
{
    public float speed = 3f; // 땅의 이동 속도
    private float groundWidth; // 땅의 가로 길이

    private void Start()
    {
        // SpriteRenderer 컴포넌트를 이용하여 땅의 가로 길이를 구합니다.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            groundWidth = spriteRenderer.bounds.size.x;
        }
    }

    private void Update()
    {
        // 땅을 왼쪽으로 이동시킵니다.
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // 땅이 왼쪽으로 벗어나면 오른쪽으로 이동합니다.
        if (transform.position.x < -groundWidth)
        {
            // 땅을 가장 오른쪽에 위치시킵니다.
            float newPositionX = transform.position.x + groundWidth * 2;
            transform.position = new Vector2(newPositionX, transform.position.y);
        }
    }
}
