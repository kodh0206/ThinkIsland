using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg14Player : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float jumpForce = 10f; // ���� �� (������ �κ�)
    public float jumpGravityScale = 0.5f; // ���� ���� ������ gravityScale
    private bool isFacingRight = true; // ���� ������ ������ ���ϰ� �ִ��� ����
    
    [SerializeField]
    private bool isJumping = false; // ���� ���� ������ ����
    
    [SerializeField]
    private bool canJump = true; // ���� ���� ����
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool monkey_right=false;
    private bool monkey_left = true;


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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // ������ �밢�� ���� ����
        if (monkey_left && canJump && (Input.GetKeyDown(KeyCode.RightArrow)|| RightButton))
        {
            FlipSprite();
            RightJump();
            monkey_left = false;
            monkey_right = true;
            RightButton=false;
        }
        if (monkey_right&& canJump && (Input.GetKeyDown(KeyCode.LeftArrow) ||LeftButton))
        {
            FlipSprite();
            LeftJump();
            monkey_left = true;
            monkey_right = false;
            LeftButton = false;
        }
    }

    private void FixedUpdate()
    {

        // ���� ���� ���� gravityScale ����
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
        // ��������Ʈ �¿� ����
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



    public void GetTree()  //������ �������.
    {
        // ������ ����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0.01f;
        isJumping = false;
        canJump = false;

        // �񵿱� ó�� ����
        StartCoroutine(DisableControlAndResetColor());
        canJump = true;
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // ���� ��Ȱ��ȭ
        enabled = false;
        
        
        // 1�ʰ� ���
        yield return new WaitForSeconds(0.1f);
        // ���� Ȱ��ȭ
        enabled = true;
        // 1�ʰ� poop ���� ���� ����
        yield return new WaitForSeconds(0.1f);
        // ���� ������� ����
    }

    public void GetHit()  //�� �¾�����
    {
        // ������ ����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0.01f;
        isJumping = true;
        canJump = false;
        // �񵿱� ó�� ����
        StartCoroutine(DisableControlAndResetColor());
    }


}
