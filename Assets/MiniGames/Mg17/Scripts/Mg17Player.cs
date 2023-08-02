using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg17Player : MonoBehaviour
{
    public GameObject stunEffect;

    public float moveSpeed = 1f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    Animator animator;
    public AudioClip shooting;
    private bool RightButton = false;
    private bool LeftButton = false;

    private bool canShoot = true;
    private float shootTimer = 0;
    private float shootInterval = 0.36f; // 0.48초에 한 번씩 사운드 재생

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
    {   
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rb.velocity.y);

        rb.velocity = movement;

        // 총소리 재생 로직
        if (canShoot)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                audioSource.PlayOneShot(shooting);
                shootTimer = 0;
            }
        }
    }

    public void GetHit()
    {
        animator.speed = 0f;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        
        canShoot = false;

        ShakeCamera();

        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // disable control
        enabled = false;



        Vector2 Effectposition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        GameObject HitEff = Instantiate(stunEffect, Effectposition, Quaternion.identity, transform);

        for (int i = 0; i < 8; i++) //Blink
        {
            Blink();
            yield return new WaitForSeconds(blinkInterval);
            BlinkEnd();
            yield return new WaitForSeconds(blinkInterval);
        }

        Destroy(HitEff);

        

        // re-enable control
        enabled = true;
        animator.Play("ShottongRmg", 0, 0);
        animator.speed = 1f;

        canShoot = true;

        
    }
    public void ShakeCamera()
    {
        Camera.main.transform.DOShakePosition(1.0f, 0.6f, 10);  // 카메라를 1초 동안, 강도 0.4로 20번 흔듭니다.
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

}