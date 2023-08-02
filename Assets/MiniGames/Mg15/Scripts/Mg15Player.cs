using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg15Player : MonoBehaviour
{
    public GameObject stunEffect;

    public float moveSpeed = 5f; 

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private SpriteRenderer spriteRenderer;
    public float blinkInterval = 0.125f; //blink
    public float minAlpha = 0.3f; // 최소 알파값 (반투명 상태)
    public float maxAlpha = 1f;   // 최대 알파값 (불투명 상태)


    Animator animator;

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
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.RightArrow) || RightButton)
        {
            horizontalInput = 1f;
            animator.SetBool("MoveRight", true);
            animator.SetBool("MoveLeft", false);
        }
        else if(Input.GetKey(KeyCode.LeftArrow) || LeftButton)
        {
            
            horizontalInput = -1f;
            animator.SetBool("MoveRight", false);
            animator.SetBool("MoveLeft", true);
        }
        else
        {
            animator.SetBool("MoveRight", false);
            animator.SetBool("MoveLeft", false);
        }

            
        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rb.velocity.y);

        
        rb.velocity = movement;
    }

    public void GetHit()
    {   
        AudioManager.Instance.Rock();

        Vector2 EffectPosition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        GameObject hitEff = Instantiate(stunEffect, EffectPosition, Quaternion.identity, transform);

        boxCollider.enabled = false;

        StartCoroutine(BlinkPlayer());

        ShakeCamera();


        StartCoroutine(EnableBoxColliderAfterDelay(0.5f));
    }

    private IEnumerator EnableBoxColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        boxCollider.enabled = true;
    }


    public void ShakeCamera()
    {
        Camera.main.transform.DOShakePosition(1.5f, 0.6f, 20);  // 카메라를 1초 동안, 강도 0.4로 20번 흔듭니다.
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
