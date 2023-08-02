using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg9Player : MonoBehaviour
{
    public Animator animator;
    public Camera myCamera;

    public SpriteRenderer spriteRenderer;

    public GameObject stunEffect;

    public float jumpForce = 12.0f;
    public float slowFallMultiplier = 0.3f;


    private Rigidbody2D rb;
    private bool isSlowFalling = false;

    private bool RightButton = false;
    private bool LeftButton = false;

    private bool IsJumping=false;

    private AudioSource audioSource;
    public AudioClip jump;

    public float blinkInterval = 0.125f; //blink
    public float minAlpha = 0.3f; // �ּ� ���İ� (������ ����)
    public float maxAlpha = 1f;   // �ִ� ���İ� (������ ����)


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
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // If AudioSource is not attached to the gameObject
        {
        audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) || RightButton)
        {
            Jump();
            RightButton = false;
        }

        // Slow Fall
        if (Input.GetKeyDown(KeyCode.Z) ||LeftButton)
        {
            SmallJump();
            StartSlowFall();
        }
        else if (Input.GetKeyUp(KeyCode.Z)||!LeftButton)
        {
            
            StopSlowFall();
        }
    }

    private void Jump()
    {   

        audioSource.PlayOneShot(jump);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void StartSlowFall()
    {
        isSlowFalling = true;
        animator.SetBool("PlayerIsWater", true);
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * slowFallMultiplier);
    }

    private void StopSlowFall()
    {
        isSlowFalling = false;
        animator.SetBool("PlayerIsWater", false);
    }

    private void SmallJump()
    {
        if (!IsJumping)
        {
            audioSource.PlayOneShot(jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce/1.5f);
            IsJumping = true;
        }
    }

    private void FixedUpdate()
    {
        if (isSlowFalling)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (slowFallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    public void GetHit()
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        rb.gravityScale = 3f;

        ShakeCamera();

        StartCoroutine(DisableControlAndResetColor());

        rb.gravityScale = 0.6f;
    }

    private IEnumerator DisableControlAndResetColor()
    {
        
        enabled = false;



        Vector2 Effectposition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        GameObject HitEff = Instantiate(stunEffect, Effectposition, Quaternion.identity, transform);
        Destroy(HitEff, 1.5f);

        for (int i = 0; i < 8; i++) //Blink
        {
            Blink();
            yield return new WaitForSeconds(blinkInterval);
            BlinkEnd();
            yield return new WaitForSeconds(blinkInterval);
        }

        

       
        enabled = true;

        

       
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsJumping = false;
            animator.SetBool("PlayerIsWater", false);

        }


    }

    public void ShakeCamera()
    {
        myCamera.transform.DOShakePosition(1.5f, 0.2f, 30);  // ī�޶� 1�� ����, ���� 0.4�� 20�� ���ϴ�.
    }


    public void Blink()
    {
        spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                minAlpha); // ������ ���·� ����


    }

    public void BlinkEnd()
    {
        spriteRenderer.color = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            maxAlpha); // ������ ���·� ����
    }

}
