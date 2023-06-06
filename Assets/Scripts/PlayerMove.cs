using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int jumpPower;
    private bool isJump=false; // ���� ���� ���� Ȯ�ο� bool�� ���� (�⺻�� : false)
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
        // Ray�� Ȱ���� Platform Ȯ�� (Ground�� ��ĵ)
        if (rigid.velocity.y < 0) //������ ���� ��ĵ
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.2f, LayerMask.GetMask("Ground"));
            if (rayHit.collider != null) // �ٴڰ� ������ ���� ���
            {
                isJump = false;
            }
        }
    }
}
