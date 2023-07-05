using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Mg4Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float jumpPower = 5.0f;
    public float moveSpeed = 5.0f;
    public float targetXPosition = -5.4f; //밀려났을 때 돌아올 곳


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

    
    public int level;
    public Mg4Player()
    {
        level = 1;
    }

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || RightButton) 
        { 
            rigidbody2D.velocity = Vector2.up * jumpPower;
            RightButton = false;
        }

        if (transform.position.x != targetXPosition)
        {
            // 현재 위치와 목표 위치를 비교하여 X 위치 이동 처리
            Vector2 targetPosition = new Vector2(targetXPosition, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime / 4f);
        }

    }
}
