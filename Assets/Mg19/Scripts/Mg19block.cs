using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19block : MonoBehaviour
{
    public GameObject breakEffect; // 부서질 때 재생될 파티클 효과

    public float moveSpeed = 5f; // 아래로 움직이는 속도

    private void Update()
    {
        // 아래로 움직임 적용
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BreakObject();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void BreakObject()
    {
        // 파티클 효과 재생
        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }

        // 오브젝트 제거
        Destroy(gameObject);
    }



    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
