using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19Player : MonoBehaviour
{
    public float jumpForce = 13f; // ���� ��
    public float moveSpeed = 5f; // ������ �ӵ�
    public float disableColliderTime = 0.3f; // ���� �� Collider ��Ȱ��ȭ �ð�
    public bool isJumping = true;

    Animator animator;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private int blockLayerMask; // Block ���̾��� ����ũ


    private bool RightButton = false;
    private bool LeftButton = false;
    private AudioSource audioSource;
    public AudioClip jump;
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
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        blockLayerMask = LayerMask.NameToLayer("Block");
        // Block 레이어와의 충돌 무시 해제
        Physics2D.IgnoreLayerCollision(gameObject.layer, blockLayerMask, false);
        isJumping = true;
        // Block 레이어 마스크 설정
        
        //audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
        float horizontalInput =  0f;
        if (Input.GetKey(KeyCode.RightArrow) || RightButton)
        {
            horizontalInput = 1f;
            animator.SetBool("RightMove", true);
            animator.SetBool("LeftMove", false);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || LeftButton)
        {

            horizontalInput = -1f;
            animator.SetBool("RightMove", false);
            animator.SetBool("LeftMove", true);
        }
        else
        {
            animator.SetBool("RightMove", false);
            animator.SetBool("LeftMove", false);
        }


        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rb.velocity.y);

        rb.velocity = movement;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {   
            //audioSource.PlayOneShot(jump);
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

        // 충돌 무시 해제
        Physics2D.IgnoreLayerCollision(gameObject.layer, blockLayerMask, false);
        isJumping = true;
        // 일정 시간이 경과한 후에 점프 상태를 체크하여 점프 가능하도록 함



    }

    private IEnumerator ResetJumping()
    {
        yield return new WaitForSeconds(0.1f);

        isJumping = true;
    }
}
