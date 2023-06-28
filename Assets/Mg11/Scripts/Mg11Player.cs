using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg11Player : MonoBehaviour
{
    // Start is called before the first frame update


    public float speed = 2.0f; // 움직임 속도
    public float radius = 2.0f; // 원의 반지름
    public float startAngle = 0.0f; // 시작 각도
    public float angleRange = 90.0f; // 허용 각도 범위

    private float angle = 0.0f; // 현재 각도

    private bool clock = false;
    private bool unclock=false;

    private void Start()
    {
        // 시작 각도 설정
        angle = startAngle;
    }

    private void Update()
    {
        // 오른쪽 버튼 입력 처리
        if (Input.GetKey(KeyCode.RightArrow))
        {
            unclock = false;
            clock= true;
        }
        // 왼쪽 버튼 입력 처리
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            clock = false;
            unclock = true;
            
        }

        if (clock)
        {
            angle += speed * Time.deltaTime;
        }
        else if (unclock)
        {
            angle -= speed * Time.deltaTime;
        }

        // 허용 각도 범위 내로 조정
        angle = Mathf.Clamp(angle, startAngle - angleRange, startAngle + angleRange);

        // 원운동 계산
        float x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        float y = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;

        // 이동
        transform.position = new Vector3(x, y, 0f);
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
