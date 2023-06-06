using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int jumpPower;
    private bool isJump=false; // 이중 점프 방지 확인용 bool값 변수 (기본값 : false)
    Rigidbody2D rigid;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Jump
        if (Input.GetButtonDown("Jump") && isJump == false)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJump = true;
        }
    }

    void FixedUpdate()
    {
        // Ray를 활용한 Platform 확인 (Ground만 스캔)
        if (rigid.velocity.y < 0) //내려갈 때만 스캔
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.2f, LayerMask.GetMask("Ground"));
            if (rayHit.collider != null) // 바닥과 접촉해 있을 경우
            {
                isJump = false;
            }
        }
    }
}
