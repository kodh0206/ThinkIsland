using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1Player : MonoBehaviour
{

    private Vector2 startPosition;
    Rigidbody2D rigid;
    public float jumpPower = 15.0f;
    bool isJump = false;
    private bool isGrounded = true;
    public bool isStunned = false;

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
/*
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJump) 
        { 
            rigid.AddForce (new Vector3(0, jumpPower, 0), ForceMode2D.Impulse);
            //isJump = true;
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision) // 충돌 감지
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJump = false;
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
}