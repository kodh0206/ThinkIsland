using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg11Player : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject stunEffect;
    public GameObject hitEff;

    public float speed = 2.0f; // 움직임 속도
    public float radius = 2.0f; // 원의 반지름
    public float startAngle = 0.0f; // 시작 각도
    public float angleRange = 90.0f; // 허용 각도 범위

    private float angle = 0.0f; // 현재 각도

    private float rotationAngle = 0f;
    private float lastAngle = 0f;

    private bool clock = false;
    private bool unclock=false;

    private bool RightButton = false;
    private bool LeftButton = false;

    private SpriteRenderer spriteRenderer;

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
        // 시작 각도 설정
        angle = startAngle;

        spriteRenderer = GetComponent<SpriteRenderer>();

        Vector2 EffectPosition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        hitEff = Instantiate(stunEffect, EffectPosition, Quaternion.identity, transform);

        // Make the HitEff invisible initially.
        SetHitEffVisibility(hitEff, false);
    }

    private void Update()
    {
        // 오른쪽 버튼 입력 처리
        if (Input.GetKey(KeyCode.RightArrow) || RightButton)
        {
            unclock = false;
            clock = true;
           
        }
        // 왼쪽 버튼 입력 처리
        else if (Input.GetKey(KeyCode.LeftArrow) || LeftButton)
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

        // 스프라이트 회전
        float rotationAngle = -angle;// 시계방향 또는 반시계방향으로 회전할 각도
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);
        transform.rotation = rotation;
    }

    public void GetHit()  //맞았을때
    {
        // 움직임 멈춤
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        ShakeCamera();

        // 비동기 처리 시작
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // 조작 비활성화
        enabled = false;

        // 색상 변경


        SetHitEffVisibility(hitEff, true);
        

        Mg11egg.instance.EggBreak();


        yield return new WaitForSeconds(2f);

        SetHitEffVisibility(hitEff, false);

        Mg11egg.instance.EggBreakEnd();

        

        // 조작 활성화
        enabled = true;

        
    }


    private void SetHitEffVisibility(GameObject hitEffect, bool isVisible)
    {
        SpriteRenderer hitEffRenderer = hitEffect.GetComponent<SpriteRenderer>();
        if (hitEffRenderer != null)
        {
            hitEffRenderer.enabled = isVisible;
        }

    }



    public void ShakeCamera()
    {
        Camera.main.transform.DOShakePosition(1.5f, 0.2f, 10);  // 카메라를 1초 동안, 강도 0.4로 20번 흔듭니다.
    }

}
