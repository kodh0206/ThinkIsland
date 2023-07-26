using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg9Player : MonoBehaviour
{
    public Animator animator;

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

        yield return new WaitForSeconds(1.5f);

       
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
        Camera.main.transform.DOShakePosition(1.5f, 0.2f, 30);  // 카메라를 1초 동안, 강도 0.4로 20번 흔듭니다.
    }

}
