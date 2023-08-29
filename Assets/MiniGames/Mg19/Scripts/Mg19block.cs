using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19block : MonoBehaviour
{
    public Animator animator;

    public GameObject breakEffect; 

    public float moveSpeed = 5f; 

    private void Update()
    {
        animator = GetComponent<Animator>();
        
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   AudioManager.Instance.BreakPlatform();
            animator.SetBool("PlayerPush", true); // 애니메이션 실행
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length+0.2f); // 애니메이션 재생 시간이 지난 후 블록 삭제
            
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void BreakObject()
    {
      
        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }

       
        Destroy(gameObject);
    }



    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
