using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19Player : MonoBehaviour
{
    public float jumpForce = 10f; // ���� ��
    public float moveSpeed = 5f; // ������ �ӵ�
    public float disableColliderTime = 0.3f; // ���� �� Collider ��Ȱ��ȭ �ð�
    public bool isJumping = true;

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
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Block ���̾��� ����ũ ����
        blockLayerMask = LayerMask.NameToLayer("Block");
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

        // ������ ���
        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rb.velocity.y);

        // ������ ����
        rb.velocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {   
            audioSource.PlayOneShot(jump);
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

        // Block ���̾���� �浹 ����
        Physics2D.IgnoreLayerCollision(gameObject.layer, blockLayerMask, true);

        yield return new WaitForSeconds(disableColliderTime);

        // �浹 ���� ������ �����ϱ� ���� ��� ���
        yield return new WaitUntil(() => rb.velocity.y <= 0f);

        // �浹 ���� ����
        Physics2D.IgnoreLayerCollision(gameObject.layer, blockLayerMask, false);

        

        isJumping = true;
    }

    private IEnumerator ResetJumping()
    {
        yield return new WaitForSeconds(0.1f);

        isJumping = true;
    }
}
