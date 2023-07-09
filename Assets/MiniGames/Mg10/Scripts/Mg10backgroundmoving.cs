using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10backgroundmoving : MonoBehaviour
{
    public float initialSpeed = 5.0f;
    public float horizontalSpeed = 10.0f;
    private float verticalSpeed;

    private Rigidbody2D rb;
    private bool isInputEnabled = true; // 입력 활성화 여부

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        verticalSpeed = initialSpeed;
    }

    private void Update()
    {
        // 입력 활성화되어 있을 때만 입력 처리
        if (isInputEnabled)
        {
            // 왼쪽키 입력 처리
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-horizontalSpeed, rb.velocity.y);
            }
            // 오른쪽키 입력 처리
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
            }

            // 입력이 없을 때
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(0f, verticalSpeed);
            }
        }
    }

    public void GetHit()
    {
        // 움직임 멈춤
        rb.velocity = new Vector2(0f, verticalSpeed);

        // 입력 비활성화
        isInputEnabled = false;

        // 비동기 처리 시작
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // 2초간 대기
        yield return new WaitForSeconds(2f);

        // 입력 활성화
        isInputEnabled = true;

        // 1초간 입력 영향 받지 않음
        yield return new WaitForSeconds(1f);
    }
}
