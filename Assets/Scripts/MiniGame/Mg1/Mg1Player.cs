using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1Player : MonoBehaviour
{
    Rigidbody2D rigid;
    public float jumpPower = 15.0f;
    bool isJump = false;

    public int level;
    public Mg1Player()
    {
        level = 1;
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
/*
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJump) 
        { 
            rigid.AddForce (new Vector3(0, jumpPower, 0), ForceMode2D.Impulse);
            //isJump = true;
        }
    }*/

    void OnCollisionEnter(Collision collision) // 충돌 감지
    {
        Debug.Log("점프");
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
        }
    }

    public void Jump(){
        if (!isJump)
        {
            rigid.AddForce (new Vector3(0, jumpPower, 0), ForceMode2D.Impulse);
            //isJump = true;
        }
    }
}
