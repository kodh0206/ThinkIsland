using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg6Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 3.0f; // 초기 점프 힘
    [SerializeField]
    private float maxJumpForce = 15.0f; // 최대 점프 힘
    [SerializeField]
    private float jumpForceIncrement = 5.0f; // 점프 힘 증가량

    [SerializeField]
    public int level=1;

    private bool isJumping = false; // 점프 중인지 여부 체크
    private bool nowJumping = false;
    private float currentJumpForce = 0.0f; // 현재 점프 힘

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping )
        {
            isJumping = true;
            currentJumpForce = jumpForce;
        }
        else if (Input.GetKey(KeyCode.Space) && isJumping )
        {
            currentJumpForce += jumpForceIncrement * Time.deltaTime;
            currentJumpForce = Mathf.Clamp(currentJumpForce, 0.0f, maxJumpForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isJumping && !nowJumping)
        {
            Jump();
            nowJumping = true;
            isJumping = false;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, currentJumpForce);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            nowJumping = false;
        }
    }

    public void GetHit()
    {
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // 조작 비활성화
        enabled = false;

        // 색상 변경
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
        }

        // 2초간 대기
        yield return new WaitForSeconds(2f);

        // 조작 활성화
        enabled = true;

        // 1초간 poop 영향 받지 않음
        yield return new WaitForSeconds(1f);

        // 색상 원래대로 복구
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
