using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mg16Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    //public bool isStunned = false;
    //public Button button1;
    //public Button button2;

    [SerializeField]
    private float moveSpeed = 5f;

    private bool moveLeft = false;
    private bool moveRight = false;

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
