using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg7Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float jumpPower = 5.0f;
    public int level;

    public Mg7Player()
    {
        level = 1;
    }

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            JumpWithAngle(135f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            JumpWithAngle(45f);
        }
    }

    void JumpWithAngle(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        Vector2 jumpDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        rigidbody2D.velocity = jumpDirection * jumpPower;
    }
}
