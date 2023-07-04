using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg9Player : MonoBehaviour
{
    public float jumpForce = 6.0f;
    public float slowFallMultiplier = 0.5f;

    private Rigidbody2D rb;
    private bool isSlowFalling = false;

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
    }

    private void Update()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) || RightButton)
        {
            Jump();
            RightButton = false;
        }

        // Slow Fall
        if (Input.GetKeyDown(KeyCode.Z) ||LeftButton)
        {
            StartSlowFall();
        }
        else if (Input.GetKeyUp(KeyCode.Z)||LeftButton)
        {
            StopSlowFall();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void StartSlowFall()
    {
        isSlowFalling = true;
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * slowFallMultiplier);
    }

    private void StopSlowFall()
    {
        isSlowFalling = false;
    }

    private void FixedUpdate()
    {
        if (isSlowFalling)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (slowFallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    public void GetHit()
    {
        // 움직임 멈춤
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        // 비동기 처리 시작
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
