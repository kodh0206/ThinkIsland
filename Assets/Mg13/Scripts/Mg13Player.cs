using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg13Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 180.0f;
    private float currentRotation;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            // 입력이 있는 경우 회전
            currentRotation -= horizontalInput * rotateSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0f, 0f, currentRotation);
        }

        // 움직임
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
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
