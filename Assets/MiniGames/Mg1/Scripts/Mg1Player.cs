using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mg1Player : MonoBehaviour
{
    public GameObject stunEffect;

    private Vector2 startPosition;
    private SpriteRenderer spriteRenderer;
    Mg1PlayerFollowGround mg1PlayerFollowGround;
    Rigidbody2D rigid;
    //public Button rightButton;
    public float jumpPower = 15.0f;
    public float moveSpeed = 5f;  // 이동 속도
    public bool isJump = false;
    public bool isGrounded = true;
    public bool isStunned = false;
    public bool isTrigger = true;
    public bool isPlayerReset = false;
    public bool RightButton = false;


    public int level;

    public AudioClip walkingSound; // 걷는소리 
    public AudioClip jumpSound;    // 점프소리
    private  AudioSource audioSource;

    public float blinkInterval = 0.125f; //blink
    public float minAlpha = 0.3f; // 최소 알파값 (반투명 상태)
    public float maxAlpha = 1f;   // 최대 알파값 (불투명 상태)

    public Mg1Player()
    {
        level = 1;
    }

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Awake()
    {
        mg1PlayerFollowGround = GetComponent<Mg1PlayerFollowGround>();
        rigid = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
    if (audioSource == null)
    {
        audioSource = gameObject.AddComponent<AudioSource>();   
    }
    }

    private void OnCollisionEnter2D(Collision2D collision) // 충돌 감지
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJump = false;
        }

        if (collision.gameObject.tag == "cow")
        {
            // 플레이어가 소보다 위에 있을 경우 (점프 + 밟음)
            if(transform.position.y > collision.transform.position.y)
            {
                isTrigger = false;
                rigid.AddForce (new Vector3(0, jumpPower, 0), ForceMode2D.Impulse);
            }
            else
            {
                isTrigger = true;
            }

            //rightButton.interactable = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.tag == "cow")
        {   
            //rightButton.interactable = true;
        }
    }

    public void JumpClick()
    {
        mg1PlayerFollowGround.isJumpButtonPressed = true;
        if (isStunned == false)
        {
            if (isGrounded && !isJump)
            {   
                audioSource.PlayOneShot(jumpSound);
                rigid.AddForce (new Vector3(0, jumpPower, 0), ForceMode2D.Impulse);
                isJump = true;
            }
        }
    }

    public void JumpClickOff()
    {
        mg1PlayerFollowGround.isJumpButtonPressed = false;
    }

    public void GetObstacle()
    {
        if (!isStunned)
        {
            StartCoroutine(DisableControlAndResetColor());
        }
    }

    private IEnumerator DisableControlAndResetColor()
    {
        
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
            //StunPlayer();
        }

        enabled = false;
        isStunned = true;

        Vector2 Effectposition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        GameObject HitEff = Instantiate(stunEffect, Effectposition, Quaternion.identity, transform);

        // Wait for 2 seconds
        for (int i = 0; i < 8; i++) //Blink
        {
            Blink();
            yield return new WaitForSeconds(blinkInterval);
            BlinkEnd();
            yield return new WaitForSeconds(blinkInterval);
        }

        Destroy(HitEff);

        enabled = true;
        isStunned = false;

        // Change color back to white
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }


    

    // rightButtonController
    public void RightClick()
    {
        RightButton = true;
        mg1PlayerFollowGround.isRightButtonPressed = true;
    }
    public void RightClickOff()
    {
        RightButton = false;
        mg1PlayerFollowGround.isRightButtonPressed = false;
    }

    private void Update()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.RightArrow) || RightButton)
        {
            horizontalInput = 1f;
        }

        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rigid.velocity.y);

        rigid.velocity = movement;

        if (transform.position.x < -12)
        {
            PlayerReset();
        }
    }

    // 플레이어 위치 리셋
    private void PlayerReset()
    {
        isPlayerReset = true;
        transform.position = new Vector2 (-6.86f, -2f);

        Mg1Manager.instance.GameLevelDown();

        Invoke("IsPlayerResetFalse", 1f);
    }

    private void IsPlayerResetFalse()
    {
        isPlayerReset = false;
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