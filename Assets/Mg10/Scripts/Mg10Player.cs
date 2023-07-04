using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float initialSpeed = 5.0f;
    public float horizontalSpeed = 10.0f;
    private float verticalSpeed;

    private Rigidbody2D rb;
    private bool isInputEnabled = true;

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
        verticalSpeed = initialSpeed;
    }

    private void Update()
    {
        if (!isInputEnabled)
        {
            rb.velocity = new Vector2(rb.velocity.x, -verticalSpeed); // 아래로 움직임
            return; // 입력 비활성화 상태면 업데이트 종료
        }

        // 왼쪽키 입력 처리
        if (Input.GetKeyDown(KeyCode.LeftArrow) || LeftButton)
        {
            rb.velocity = new Vector2(-horizontalSpeed, rb.velocity.y);
        }
        // 오른쪽키 입력 처리
        else if (Input.GetKeyDown(KeyCode.RightArrow) ||RightButton)
        {
            rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
        }

        // 입력이 없을 때
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, -verticalSpeed);
        }
    }


    public void GetHit()  //맞았을때
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
