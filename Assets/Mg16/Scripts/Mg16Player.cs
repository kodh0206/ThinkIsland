using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mg16Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    [SerializeField]
    private float moveSpeed = 5f;

    private bool moveLeft = false;
    private bool moveRight = false;
    public float positionPlayer;  // x축 위치를 저장할 변수

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Button1Pressed()
    {
        moveLeft = true;
        moveRight = false;
    }

    public void Button2Pressed()
    {
        moveLeft = false;
        moveRight = true;
    }

    private void Update()
    {
        if (moveLeft)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        else if (moveRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        positionPlayer = transform.position.x; // x축 위치 저장
    }

    public void IncreaseSpeed()
    {
        if (moveSpeed <= 11.0f)
        {
            moveSpeed += 2.0f;
        }
        else
        {
            moveSpeed = 11.0f;
        }
    }
}
