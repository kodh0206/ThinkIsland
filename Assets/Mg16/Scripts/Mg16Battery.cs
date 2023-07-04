using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg16Battery : MonoBehaviour
{

    Mg16BatterySpawner mg16BatterySpawner;
    public Sprite batterySprite;  // 던질 때 스프라이트 이미지
    public Sprite waterBatterySprite;  // 물에 빠진 배터리 스프라이트 이미지
    public Animator animator;  // 애니메이터 컴포넌트
    public Tween movementTween;  // Tween 변수 추가

    private SpriteRenderer spriteRenderer;

    public float startY = 1.7f;
    public float topY = 2.5f;
    // 배터리 끝 위치
    public float stopY = -3.5f;
    // 해당 위치까지 이동하는 데 걸리는 시간
    public float moveDuration = 1.5f;
    

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();  // 애니메이터 컴포넌트 가져오기
        mg16BatterySpawner = GetComponent<Mg16BatterySpawner>();
        spriteRenderer.sprite = batterySprite;  // 초기 스프라이트를 물에 안 빠진 배터리로 설정
        StartMovement();
    }

    private void Update()
    {
        if (transform.position.y < stopY+1f)
        {
            animator.SetBool("BatteryInWater", true); // 기존 애니메이션 재생 정지
            ChangeSprite();  // 스프라이트 변화 함수 호출
            // 한 세트 - 이동 시간만큼 시간 지연
            //Invoke("StopMovement", 1f);//mg16BatterySpawner.time_diff - (moveDuration*2));
        }
    }

    private void ChangeSprite()
    {
        spriteRenderer.sprite = waterBatterySprite;  // 물에 빠진 배터리 스프라이트로 변경
    }

    private void StartMovement()
    {
        transform.DOMoveY(topY, moveDuration).SetEase(Ease.Linear).OnComplete(ReverseMovement);
    }

    private void ReverseMovement()
    {
        transform.DOMoveY(stopY, moveDuration).SetEase(Ease.Linear);
    }

    private void StopMovement()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}