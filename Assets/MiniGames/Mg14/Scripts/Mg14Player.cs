using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg14Player : MonoBehaviour
{

    public Camera myCamera;

    public GameObject stunEffect;

    public float moveSpeed = 5f; 
    public float jumpForce = 10f; 
    public float jumpGravityScale = 0.5f; 
    private bool isFacingRight = true; 
    
    [SerializeField]
    private bool isJumping = false; 
    
    [SerializeField]
    private bool canJump = true; 
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool monkey_right=false;
    private bool monkey_left = true;


    private bool RightButton = false;
    private bool LeftButton = false;

    private AudioSource audioSource;
    public AudioClip monkeyjump;
    public AudioClip monkeyfall;

    public float blinkInterval = 0.125f; //blink
    public float minAlpha = 0.3f; // 최소 알파값 (반투명 상태)
    public float maxAlpha = 1f;   // 최대 알파값 (불투명 상태)


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
    {   audioSource =GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
        if (monkey_left && canJump && (Input.GetKeyDown(KeyCode.RightArrow)|| RightButton))
        {
            monkey_left = false;
            monkey_right = true;
            RightButton = false;
            canJump = false;
            FlipSprite();
            RightJump();
            
        }
        if (monkey_right&& canJump && (Input.GetKeyDown(KeyCode.LeftArrow) ||LeftButton))
        {
            monkey_left = true;
            monkey_right = false;
            LeftButton = false;
            canJump = false;
            FlipSprite();
            LeftJump();
            
        }
    }

    private void FixedUpdate()
    {

        
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
        
        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !isFacingRight;
    }

    private void RightJump()
    {
        if (!isJumping)
        {   
            float jumpForceX = jumpForce * Mathf.Cos(Mathf.PI / 6); 
            float jumpForceY = jumpForce * Mathf.Sin(Mathf.PI / 6); 
            if(AudioManager.Instance.isSFXOn)
            {
            audioSource.PlayOneShot(monkeyjump);
            }
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
            if(AudioManager.Instance.isSFXOn)
            {
            audioSource.PlayOneShot(monkeyjump);
            }
            rb.AddForce(new Vector2(-jumpForceX, jumpForceY), ForceMode2D.Impulse);
            isJumping = true;
        }
    }



    public void GetTree()  
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0.01f;
        isJumping = false;
        canJump = false;

        

        StartCoroutine(DisableControlAndResetColor());
        canJump = true;
    }

    private IEnumerator DisableControlAndResetColor()
    {
        
        enabled = false;
        
        
        
        yield return new WaitForSeconds(0.1f);
        
        enabled = true;
        
        yield return new WaitForSeconds(0.1f);
        
    }

    public void GetHit()  
    {
        Vector2 EffectPosition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        GameObject hitEff = Instantiate(stunEffect, EffectPosition, Quaternion.identity, transform);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0.01f;
        if(AudioManager.Instance.isSFXOn)
        {
        audioSource.PlayOneShot(monkeyfall);
        }
        isJumping = true;
        canJump = false;

        ShakeCamera();



        RotateOneCircle();

        StartCoroutine(BlinkPlayer());

        StartCoroutine(DisableControlAndResetColor());
    }

    public void ShakeCamera()
    {
        myCamera.transform.DOShakePosition(1.5f, 0.6f, 15);  // 카메라를 1초 동안, 강도 0.4로 20번 흔듭니다.
    }


    private void RotateOneCircle()
    {
        // DOTween을 사용하여 오브젝트를 한 바퀴 회전시킵니다.
        transform.DORotate(new Vector3(0f, 0f, 360f), 1.5f, RotateMode.LocalAxisAdd)
            .SetEase(Ease.OutQuint); // 회전에 사용할 움직임(Ease)을 설정합니다.
    }

    public void Blink()
    {
        spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                minAlpha); // 반투명 상태로 설정
    }

    public void BlinkEnd()
    {
        spriteRenderer.color = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            maxAlpha); // 불투명 상태로 설정
    }

    private IEnumerator BlinkPlayer()
    {
        for (int i = 0; i < 4; i++) //Blink
        {
            Blink();
            yield return new WaitForSeconds(blinkInterval);
            BlinkEnd();
            yield return new WaitForSeconds(blinkInterval);
        }
    }

}
