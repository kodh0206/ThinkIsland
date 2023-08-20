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

    public float jumpForce = 10.0f;
    public float slowFallMultiplier = 0.3f;


    private Rigidbody2D rb;
    private bool isSlowFalling = false;

    private bool RightButton = false;
    private bool LeftButton = false;

    public bool IsJumping=false;

    private AudioSource audioSource;
    public AudioClip jump;

    public float blinkInterval = 0.125f; //blink
    public float minAlpha = 0.3f; 
    public float maxAlpha = 1f;


    public float gravityChangeDuration ; // 원하는 서서히 변화하는 시간 (초)
    public float targetGravityScale ; // 목표로 하는 gravityScale 값

    public bool isGravityChanging = false;
    private float gravityChangeStartTime;
    private float initialGravityScale;

    public ParticleSystem particlePrefab;//Particle
    private ParticleSystem currentParticle;


    public float targetXPosition = -7.0f;
    public float moveSpeed = 5f;

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
        if (!IsJumping && Input.GetKeyDown(KeyCode.Space) || RightButton)
        {
            if (!IsJumping)
            {
                Jump();
                RightButton = false;
            }
            RightButton = false;
        }

        // Slow Fall
        if (Input.GetKeyDown(KeyCode.Z) ||LeftButton)
        {
            
            
            if (IsJumping && !isGravityChanging &&!isSlowFalling) 
            {
                animator.SetBool("PlayerIsWater", true);
                CreateParticle();
                isSlowFalling = true;
                StartSlowUp();
                
            }
        }
        else if (!LeftButton)
        {
            animator.SetBool("PlayerIsWater", false);
            StopSlow();
            
        }

        if (transform.position.x != targetXPosition)
        {

            Vector2 targetPosition = new Vector2(targetXPosition, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime / 4f);
        }

    }

    private void FixedUpdate()
    {
        if (isGravityChanging)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (slowFallMultiplier - 1) * Time.fixedDeltaTime;
        }
        if (isGravityChanging)
        {
            float timeSinceStart = Time.time - gravityChangeStartTime;
            float t = Mathf.Clamp01(timeSinceStart / gravityChangeDuration);
            rb.gravityScale = Mathf.Lerp(initialGravityScale, targetGravityScale, t);
            if (rb.gravityScale == targetGravityScale)
            {
                Debug.Log("추락시작");
                isGravityChanging = false;
                if (rb.gravityScale<=0f)
                {
                    StartSlowDown();
                }
                
            }
        }
    }

    private void Jump()
    {
        if (AudioManager.Instance.isSFXOn)
        {
            audioSource.PlayOneShot(jump);
        }
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        
    }

    private void StartSlowUp()
    {
        rb.velocity *= 0.1f;

        gravityChangeDuration = 0.1f;
        targetGravityScale = -0.5f;
        initialGravityScale = 0.5f;
        gravityChangeStartTime = Time.time;
        isGravityChanging = true;

    }

    private void StartSlowDown()
    {
        gravityChangeDuration = 0.5f;
        targetGravityScale = 0.5f;
        initialGravityScale = -0.5f;
        gravityChangeStartTime = Time.time;
        isGravityChanging = true;

    }

    private void StopSlow()
    {
        isGravityChanging = false;
        isSlowFalling=false;
        rb.gravityScale = 0.5f;
    }


    public void GetHit()
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        rb.gravityScale = 3f;

        ShakeCamera();

        StartCoroutine(DisableControlAndResetColor());

        rb.gravityScale = 0.5f;
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsJumping = true;

        }
    }

    public void ShakeCamera()
    {
        myCamera.transform.DOShakePosition(1.5f, 0.2f, 30);  
    }


    public void Blink()
    {
        spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                minAlpha); 


    }

    public void BlinkEnd()
    {
        spriteRenderer.color = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            maxAlpha); 
    }



    private void CreateParticle()
    {
        // 파티클을 씬에 생성하고 파티클 컴포넌트를 저장할 변수에 할당
        currentParticle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        // 파티클 재생
        currentParticle.Play();

        // 일정 시간이 지난 후 파티클을 자동으로 제거
        Destroy(currentParticle.gameObject, 2f);
    }

}
