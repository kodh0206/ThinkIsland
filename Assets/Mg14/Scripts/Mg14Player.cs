using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg14Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 10f; // 점프 힘 (수정된 부분)
    public float jumpGravityScale = 0.5f; // 점프 중인 동안의 gravityScale

    private bool isFacingRight = true; // 현재 오른쪽 방향을 향하고 있는지 여부

    [SerializeField]
    private bool isJumping = false; // 현재 점프 중인지 여부
    [SerializeField]
    private bool canJump = true; // 점프 가능 여부

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool monkey_right=false;
    private bool monkey_left = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {


        // 오른쪽 대각선 위로 점프
        if (monkey_left && canJump && Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) )
        {
            FlipSprite();
            RightJump();
            monkey_left = false;
            monkey_right = true;
        }

        if (monkey_right&& canJump && Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) )
        {
            FlipSprite();
            LeftJump();
            monkey_left = true;
            monkey_right = false;
        }


    }

    private void FixedUpdate()
    {

        // 점프 중인 동안 gravityScale 조정
        if (isJumping)
        {
            rb.gravityScale = jumpGravityScale;
        }
        else
        {
            rb.gravityScale = 0.1f;
        }
    }

    private void FlipSprite()
    {
        // 스프라이트 좌우 반전
        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !isFacingRight;
    }

    private void RightJump()
    {
        if (!isJumping)
        {
            float jumpForceX = jumpForce * Mathf.Cos(Mathf.PI / 6); 
            float jumpForceY = jumpForce * Mathf.Sin(Mathf.PI / 6); 
            rb.AddForce(new Vector2(jumpForceX, jumpForceY), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void LeftJump()
    {
        if (!isJumping)
        {
            float jumpForceX = jumpForce * Mathf.Cos(Mathf.PI / 10);
            float jumpForceY = jumpForce * Mathf.Sin(Mathf.PI / 10);
            rb.AddForce(new Vector2(-jumpForceX, jumpForceY), ForceMode2D.Impulse);
            isJumping = true;
        }
    }



    public void GetTree()  //나무를 잡았을때.
    {
        // 움직임 멈춤
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0.01f;
        isJumping = false;
        canJump = false;

        // 비동기 처리 시작
        StartCoroutine(DisableControlAndResetColor());
        canJump = true;
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // 조작 비활성화
        enabled = false;

        
        
        // 1초간 대기
        yield return new WaitForSeconds(0.1f);

        // 조작 활성화
        enabled = true;

        // 1초간 poop 영향 받지 않음
        yield return new WaitForSeconds(0.1f);

        // 색상 원래대로 복구

    }

    public void GetHit()  //돌 맞았을때
    {
        // 움직임 멈춤
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0.01f;
        isJumping = true;
        canJump = false;

        // 비동기 처리 시작
        StartCoroutine(DisableControlAndResetColor());
    }


}
