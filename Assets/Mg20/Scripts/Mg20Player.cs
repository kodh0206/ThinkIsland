using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20Player : MonoBehaviour
{
    
    public float moveSpeed = 5f; // ������ �ӵ�
    

    private Rigidbody2D rb;
    



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        

       
        
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // ������ ���
        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rb.velocity.y);

        // ������ ����
        rb.velocity = movement;
    }
}
