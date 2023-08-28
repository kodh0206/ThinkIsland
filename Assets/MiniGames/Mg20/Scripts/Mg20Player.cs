using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20Player : MonoBehaviour
{
    
    public float moveSpeed = 5f; // 움직임 속도
    

    private Rigidbody2D rb;

    private bool RightButton = false;
    private bool LeftButton = false;

    public void RightClick()
    {
        LeftButton = false;
        RightButton = true;

    }

    public void RightClickOff()
    {
        RightButton = false;

    }

    public void LeftClick()
    {
        RightButton = false;
        LeftButton = true;
    }

    public void LeftClickOff()
    {
        LeftButton = false;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }


    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");


        if (RightButton)
        {
            horizontalInput = 1.1f;
        }
        else if (LeftButton)
        {
            horizontalInput = -1.1f;
        }

        // 움직임 계산
        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rb.velocity.y);

        // 움직임 적용
        rb.velocity = movement;
        if (rb.velocity.y < -5f)
        {
            rb.velocity = new Vector2(moveX, -5f); ;
        }
    }
}
