using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg6Player : MonoBehaviour
{
    // 버튼 누르는 시간에 비례해 바뀌는 개구리 색상(더 빨개지도록)
    public SpriteRenderer spriteRenderer;
    public float duration = 2.0f; // 변화에 걸리는 시간
    private bool isPressed = false;
    private float elapsedTime = 0.0f;
    private Color startColor;
    private Color targetColor;

    public float blinkInterval = 0.125f; //blink
    public float minAlpha = 0.3f; // 최소 알파값 (반투명 상태)
    public float maxAlpha = 1f;   // 최대 알파값 (불투명 상태)

    public GameObject stunEffect;

    [SerializeField]
    private float jumpForce = 3.0f; // �ʱ� ���� ��
    [SerializeField]
    private float maxJumpForce = 15.0f; // �ִ� ���� ��
    [SerializeField]
    private float jumpForceIncrement = 5.0f; // ���� �� ������

    [SerializeField]
    public int level=1;

    private bool isJumping = false; // ���� ������ ���� üũ
    private bool nowJumping = false;
    private float currentJumpForce = 0.0f; // ���� ���� ��
    
    public AudioClip shortJumpSound; // Assign the short jump sound in the Inspector
    public AudioClip longJumpSound; // Assign the long jump sound in the Inspector

    private AudioSource audioSource;

    private Rigidbody2D rb;
    Animator anim;

    private bool RightButton = false;
    private bool LeftButton = false;
    private bool PushingButton = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // If AudioSource is not attached to the gameObject
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = Color.white;
        targetColor = Color.red;
        
    }

    public void RightClick()
    {
        LeftButton = false;
        RightButton = true;
        PushingButton = true;

    }

    public void RightClickOff()
    {
        
        PushingButton = false;
    }

    public void LeftClick()
    {
        RightButton = false;
        LeftButton = true;
        PushingButton = true;

    }

    public void LeftClickOff()
    {
        LeftButton = false;
        PushingButton = false;

    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)|| (RightButton &&PushingButton)) && !isJumping && !nowJumping)
        {
            isJumping = true;
            currentJumpForce = jumpForce;
            anim.SetBool("isSitting", true);
            isPressed = true;
            elapsedTime = 0.0f;
        }
        else if ((Input.GetKey(KeyCode.Space) || (RightButton && PushingButton)) && isJumping && !nowJumping)
        {
            currentJumpForce += jumpForceIncrement * Time.deltaTime;
            currentJumpForce = Mathf.Clamp(currentJumpForce, 0.0f, maxJumpForce);

            // 스프라이트 잠시 바꾸기
        }

        if (((Input.GetKeyUp(KeyCode.Space) || (RightButton && !PushingButton))) && isJumping && !nowJumping)
        {
            Jump();
            nowJumping = true;
            isJumping = false;
            RightButton = false;
            isPressed = false;
            elapsedTime = 0.0f;

        }

        // 개구리 색상 변화
        if (isPressed)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            // 보간된 색상 계산
            Color lerpedColor = Color.Lerp(startColor, targetColor, t);

            // SpriteRenderer의 색상 변경
            spriteRenderer.color = lerpedColor;
        }
        else if (spriteRenderer.color != startColor)
        {
            // 버튼을 누르지 않은 경우 즉시 흰색으로 변경
            spriteRenderer.color = startColor;
        }

    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, currentJumpForce);
        anim.SetBool("isJumping", true);

    if (currentJumpForce > maxJumpForce / 2)
    {
        audioSource.PlayOneShot(longJumpSound);
    }
    else
    {
        audioSource.PlayOneShot(shortJumpSound);
    }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            nowJumping = false;
            anim.SetBool("isJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         if (other.CompareTag("Ground"))
        {
            anim.SetBool("isSitting", false);
        }
    }

    public void GetHit()
    {
        ShakeCamera();
        
        StartCoroutine(DisableControlAndResetColor());

    }

    private IEnumerator DisableControlAndResetColor()
    {
        // can`t controll
        enabled = false;

        Mg6manager.instance.GameLevelDown();


        Vector2 Effectposition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        GameObject HitEff = Instantiate(stunEffect, Effectposition, Quaternion.identity, transform);
        Destroy(HitEff,1.0f);

        rb.gravityScale = 3.0f;


        for (int i = 0; i < 8; i++) //Blink
        {
            Blink();
            yield return new WaitForSeconds(blinkInterval);
            BlinkEnd();
            yield return new WaitForSeconds(blinkInterval);
        }
             

        rb.gravityScale = 1.0f;

        enabled = true;


        
    }


    public void ShakeCamera()
    {
        Camera.main.transform.DOShakePosition(0.8f, 0.4f ,20);  // 카메라를 1초 동안, 강도 1로 1번 흔듭니다.
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
