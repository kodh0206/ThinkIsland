using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg18Player : MonoBehaviour
{
    public float maxGravityScale = 1f; // 최대 gravity scale 값

    public float moveSpeed = 5f; // 움직임 속도

    private float jumpForce = 12.0f; // 초기 점프 힘

    public float targetXPosition = -7.5f; //밀려났을 때 돌아올 곳

    private bool isHit = false;

    private BoxCollider2D boxCollider;

    [SerializeField]
    private bool nowJumping = false;

    private Rigidbody2D rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // 시작할 때 gravity scale을 0로 설정
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            rb.gravityScale = Mathf.Clamp(rb.gravityScale, 0f, maxGravityScale);
        }

        // y 좌표가 음수인 경우 gravity scale 조정
        if (!(isHit) && transform.position.y <= 0 )
        {
            float gravityScale = transform.position.y; // y 좌표의 절댓값을 gravity scale로 사용
            gravityScale = Mathf.Clamp(gravityScale, -maxGravityScale, 0f ); // 최대값 제한
            rb.gravityScale = gravityScale/ Random.Range(1f,3f);
 
        }
        else
        {
            rb.gravityScale = 1.0f;
        }

        if (!(isHit) &&  Input.GetKey(KeyCode.LeftArrow) && !nowJumping) // 땅을 밟았거나 물 속 일때.
        {
            Jump();
            nowJumping = true;
            
        }

        if (!(isHit) && transform.position.x != targetXPosition)
        {
            // 현재 위치와 목표 위치를 비교하여 X 위치 이동 처리
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
        if (other.gameObject.tag == "Ground" )
        {
            nowJumping = false;
        }
        if (other.gameObject.tag == "water")
        {
            nowJumping = false;
        }

    }

    public void GetHit()
    {
        // 움직임 멈춤
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        isHit= true;

        // 비동기 처리 시작
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
