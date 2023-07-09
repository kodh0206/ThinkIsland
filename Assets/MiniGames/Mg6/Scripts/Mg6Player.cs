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
        if ((Input.GetKeyDown(KeyCode.Space)|| (RightButton &&PushingButton)) && !isJumping )
        {
            isJumping = true;
            currentJumpForce = jumpForce;
            anim.SetBool("isSitting", true);
            isPressed = true;
            elapsedTime = 0.0f;
        }
        else if ((Input.GetKey(KeyCode.Space) || (RightButton && PushingButton)) && isJumping )
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
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // ���� ��Ȱ��ȭ
        enabled = false;

        // ���� ����
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
        }

        // 2�ʰ� ���
        yield return new WaitForSeconds(2f);

        // ���� Ȱ��ȭ
        enabled = true;

        // 1�ʰ� poop ���� ���� ����
        yield return new WaitForSeconds(1f);

        // ���� ������� ����
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}