using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg18Player : MonoBehaviour
{
    public Animator animator;  // 애니메이터 컴포넌트
    public float maxGravityScale = 1f; // �ִ� gravity scale ��

    public float moveSpeed = 5f; // ������ �ӵ�

    private float jumpForce = 12.0f; // �ʱ� ���� ��

    public float targetXPosition = -7.5f; //�з����� �� ���ƿ� ��

    private bool isHit = false;

    private BoxCollider2D boxCollider;

    [SerializeField]
    private bool nowJumping = false;

    private Rigidbody2D rb;

    public AudioClip splashSound;   // 첨벙소리 오디오 클립
    public AudioClip jumpSound;     // 점프 소리 오디오 클립
    public AudioSource audioSource;
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
        animator = GetComponent<Animator>();  // 애니메이터 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // ������ �� gravity scale�� 0�� ����
        audioSource = GetComponent<AudioSource>();
        boxCollider =GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || RightButton)
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            rb.gravityScale = Mathf.Clamp(rb.gravityScale, 0f, maxGravityScale);
        }

        // y ��ǥ�� ������ ��� gravity scale ����
        if (!(isHit) && transform.position.y <= 0 )
        {   
             audioSource.clip = splashSound;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            float gravityScale = transform.position.y; // y ��ǥ�� ������ gravity scale�� ���
            gravityScale = Mathf.Clamp(gravityScale, -maxGravityScale, 0f ); // �ִ밪 ����
            rb.gravityScale = gravityScale/ Random.Range(1f,3f);
            animator.SetBool("PlayerIsWater", true);
        }
        else
        {   
        if (!(nowJumping))
        {
            
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            }
            else{
                audioSource.Stop();
            }
            rb.gravityScale = 1.0f;
            animator.SetBool("PlayerIsWater", false);
        }

        if ((Input.GetKey(KeyCode.LeftArrow) || LeftButton) &&!(isHit)  && !nowJumping ) // ���� ��Ұų� �� �� �϶�.
        {   

            audioSource.clip = jumpSound;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            Jump();
            LeftButton= false;
            
        }

        if (!(isHit) && transform.position.x != targetXPosition)
        {
            // ���� ��ġ�� ��ǥ ��ġ�� ���Ͽ� X ��ġ �̵� ó��
            Vector2 targetPosition = new Vector2(targetXPosition, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime/4f);
        }

    }


    private void Jump()
    {
        rb.gravityScale = 0.5f;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "water")
        {
            nowJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "water")
        {
            nowJumping = true;
        }
    }

    public void GetHit()
    {
        // ������ ����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        isHit= true;

        // �񵿱� ó�� ����
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        enabled = false;
        rb.gravityScale = 2f;
        boxCollider.enabled = true;

        yield return new WaitForSeconds(2f);

        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }



}
