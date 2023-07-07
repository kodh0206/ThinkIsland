using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1Player : MonoBehaviour
{

    private Vector2 startPosition;
    Rigidbody2D rigid;
    public float jumpPower = 15.0f;
    public float moveSpeed = 5f;  // 이동 속도
    bool isJump = false;
    private bool isGrounded = true;
    public bool isStunned = false;
    public bool isTrigger = true;
    private bool RightButton = false;


    public int level;

    public AudioClip walkingSound; // 걷는소리 
    public AudioClip jumpSound;    // 점프소리
    private  AudioSource audioSource;
    public Mg1Player()
    {
        level = 1;
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Awake()
    {
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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void Jump()
    {
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

    public void GetObstacle()
    {
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
            //StunPlayer();
        }

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Change color back to white
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }


    public void StunPlayer()
    {
        isStunned = true;
        StartCoroutine(RecoverFromStun());
    }

    private IEnumerator RecoverFromStun()
    {
        yield return new WaitForSeconds(2f);
        isStunned = false;
    }

    // rightButtonController
    public void RightClick()
    {
        RightButton = true;
    }
    public void RightClickOff()
    {
        RightButton = false;
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
    }
}