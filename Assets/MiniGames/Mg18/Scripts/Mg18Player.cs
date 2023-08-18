using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg18Player : MonoBehaviour
{
    public Animator animator;  // 애니메이터 컴포넌트
    public float maxGravityScale = 1f; 

    public float moveSpeed = 5f; 

    private float jumpForce = 12.0f; 

    public float targetXPosition = -7.5f; 

    private bool isHit = false;

    private BoxCollider2D boxCollider;

    private bool nowJumping = false;

    private bool CanJumping = false;
    public int jumpCount = 2;

    private Rigidbody2D rb;

    public AudioClip splashSound;   // 첨벙소리 오디오 클립
    public AudioClip jumpSound;     // 점프 소리 오디오 클립
    public AudioSource audioSource;
    private bool RightButton = false;
    private bool LeftButton = false;

    public ParticleSystem particlePrefab;//Particle
    private ParticleSystem currentParticle;

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
        rb.gravityScale = 0f; 
        audioSource = GetComponent<AudioSource>();
        boxCollider =GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || RightButton)
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            rb.gravityScale = Mathf.Clamp(rb.gravityScale/2f, 0f, maxGravityScale);
        }


        if ( transform.position.y <= -2.0f)
        {
            // audioSource.clip = splashSound;
            //if (!audioSource.isPlaying &&AudioManager.Instance.isSFXOn)
            //{
            //    audioSource.Play();
            //}
            //float gravityScale = transform.position.y + 1.5f;
            //gravityScale = Mathf.Clamp(gravityScale / 2f, -maxGravityScale / 1.5f, 0f);
            rb.gravityScale = Random.Range(-1.0f, 0.0f);
            if (rb.velocity.y <= -2f)
            {
                rb.velocity = new Vector2(0, -2f);
            }
            
            

            jumpForce = 30f;
        }

        else
        {
            jumpForce = 12f;
            if (!(nowJumping))
            {

                //if (!audioSource.isPlaying &&AudioManager.Instance.isSFXOn)
                //{
                //    audioSource.Play();
                //}
                //}
                //else{
                //    audioSource.Stop();
                //}
                rb.gravityScale = 1.0f;
            }

            if ((Input.GetKey(KeyCode.LeftArrow) || LeftButton) && !(isHit))
            {

                //audioSource.clip = jumpSound;
                //if (!audioSource.isPlaying &&AudioManager.Instance.isSFXOn)
                //{
                //    audioSource.Play();
                //}
                if (jumpCount != 0)
                {
                    jumpCount -= 1;
                    Jump();
                    
                }
                LeftButton = false;

            }

            if (transform.position.x != targetXPosition)
            {

                Vector2 targetPosition = new Vector2(targetXPosition, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime / 4f);
            }
        }
    }

    

    private void Jump()
    {
       
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" )
        {
            nowJumping = false;
            jumpCount = 2;
        }
    }
    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            nowJumping = true;
            jumpCount = 1;
        }
    }



    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "water")
        {
            animator.SetBool("PlayerIsWater", true);
            CreateParticle();
            nowJumping = false;
            jumpCount = 2;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "water")
        {
            animator.SetBool("PlayerIsWater", false);
            CreateParticle();
            nowJumping = true;
        }
    }



    public void GetHit()
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        isHit= true;

        
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

    private void CreateParticle()
    {
        // 파티클을 씬에 생성하고 파티클 컴포넌트를 저장할 변수에 할당
        currentParticle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        // 파티클 재생
        currentParticle.Play();

        // 일정 시간이 지난 후 파티클을 자동으로 제거
        Destroy(currentParticle.gameObject, 0.5f);
    }

}
