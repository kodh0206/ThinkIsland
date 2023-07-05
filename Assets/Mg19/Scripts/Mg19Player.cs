using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19Player : MonoBehaviour
{
    public float jumpForce = 10f; // 점프 힘
    public float moveSpeed = 5f; // 움직임 속도
    public float disableColliderTime = 0.3f; // 점프 전 Collider 비활성화 시간
    public bool isJumping = true;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private int blockLayerMask; // Block 레이어의 마스크


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

        // Block 레이어의 마스크 설정
        blockLayerMask = LayerMask.NameToLayer("Block");
    }

    private void Update()
    {
        rb.gravityScale = 1f;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {
            Jump();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(ResetJumping());
        }
    }

    private void Jump()
    {
        StartCoroutine(DisableColliderForJump());

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        
    }

    private IEnumerator DisableColliderForJump()
    {
        isJumping = false;

        // Block 레이어와의 충돌 무시
        Physics2D.IgnoreLayerCollision(gameObject.layer, blockLayerMask, true);

        yield return new WaitForSeconds(disableColliderTime);

        // 충돌 무시 설정을 해제하기 위해 잠시 대기
        yield return new WaitUntil(() => rb.velocity.y <= 0f);

        // 충돌 무시 해제
        Physics2D.IgnoreLayerCollision(gameObject.layer, blockLayerMask, false);

        rb.gravityScale = 1.5f;

        isJumping = true;
    }

    private IEnumerator ResetJumping()
    {
        yield return new WaitForSeconds(0.1f);

        isJumping = true;
    }
}
