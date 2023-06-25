using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Mg8Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float diagonalSpeedMultiplier = 0.7f;
    public int level;
    private Rigidbody2D rb;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public Mg8Player()
    {
        level = 1;
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        if (Input.GetKey(KeyCode.X))
        {
            moveDirection = new Vector2(1f, 1f);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            moveDirection = new Vector2(-1f, -1f);
        }

        rb.velocity = moveDirection * speed * (Mathf.Abs(horizontalInput) > 0.5f || Mathf.Abs(verticalInput) > 0.5f ? diagonalSpeedMultiplier : 1f);
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
