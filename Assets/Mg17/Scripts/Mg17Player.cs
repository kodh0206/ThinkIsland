using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg17Player : MonoBehaviour
{
    public float moveSpeed = 1f; // 움직임 속도

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    Animator animator;

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
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (RightButton)
        {
            horizontalInput = 1f;
        }
        else if (LeftButton)
        {
            horizontalInput = -1f;
        }

        // 움직임 계산
        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rb.velocity.y);

        // 움직임 적용
        rb.velocity = movement;
    }


    public void GetHit()
    {
        // 움직임 멈춤
        animator.speed = 0f;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        // 비동기 처리 시작
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // 조작 비활성화
        enabled = false;

        // 색상 변경
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
        }

        // 2초간 대기
        yield return new WaitForSeconds(2f);

        // 조작 활성화
        enabled = true;
        animator.Play("ShottongRmg", 0, 0);
        animator.speed = 1f;

        // 1초간 poop 영향 받지 않음
        yield return new WaitForSeconds(1f);

        // 색상 원래대로 복구
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
