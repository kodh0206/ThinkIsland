using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20Player : MonoBehaviour
{
    
    public float moveSpeed = 5f; // 움직임 속도
    

    private Rigidbody2D rb;
    



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        

       
        
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // 움직임 계산
        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rb.velocity.y);

        // 움직임 적용
        rb.velocity = movement;
    }
}
