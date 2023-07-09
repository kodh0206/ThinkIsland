using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Mg2Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;


    private Animator anim;
    private float originalSpeed; // 기존 애니메이션 재생 속도
    public Button button1;
    public Button button2;
    public float duration = 1f; // 이동에 걸리는 시간
    public float height = 2f; // 포물선의 높이

    // 목표 좌표
    public float targetLeft = -4.3f;
    public float targetCenter = 0f;
    public float targetRight = 4.3f;

    public float targetX;

    // 좌우 반전 관련 변수
    private bool isFacingRight = true;
    public bool isStunned = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSpeed = anim.speed; // 기존 애니메이션 재생 속도 저장
    }

    private void Update()
    {

        // 플레이어 위치 근삿값 확인 -> 0으로 초기화
        if (Mathf.Approximately(transform.position.x, 2.384186e-07f))
        {
            transform.position = Vector2.zero;
        }

        if (Mathf.Approximately(transform.position.x, -2.384186e-07f))
        {
            transform.position = Vector2.zero;
        }
    }

    public void LeftButtonPressed()
    {
        if (transform.position.x == 0)
        {
            PlayerMove(-4.3f);
        }

        if (transform.position.x == 4.3f)
        {
            PlayerMove(0f);
        }

        // 플레이어가 오른쪽을 바라보고 있는 경우
        if (!spriteRenderer.flipX)
        {
            Flip();
        }
    }

    public void RightButtonPressed()
    {
        if (transform.position.x == 0)
        {
            PlayerMove(4.3f);
        }
        if (transform.position.x == -4.3f)
        {
            PlayerMove(0f);
        }

        // 플레이어가 왼쪽을 바라보고 있는 경우
        if (spriteRenderer.flipX)
        {
            Flip();
        }
    }

    public void PlayerMove(float targetX)
    {
        // 포물선 동작을 위한 시작 위치와 중간 지점 계산
        Vector3 startPos = transform.position;
        Vector3 midPos = new Vector3((startPos.x + targetX) / 2f, startPos.y + height, startPos.z);

        // 뛰는 애니메이션 재생
        anim.SetBool("isJumping", true);

        // DOTween을 사용하여 포물선 이동 애니메이션 생성
        transform.DOPath(new Vector3[] { startPos, midPos, new Vector3(targetX, startPos.y, startPos.z) }, duration, PathType.CatmullRom)
            .SetEase(Ease.OutQuad) // 애니메이션 이징 설정
            .OnComplete(OnComplete); // 애니메이션 완료 후 호출될 콜백 함수 설정
    }

    private void OnComplete()
    {
        anim.SetBool("isJumping", false);
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight; // 방향을 반전시킴

        // 스프라이트 렌더러의 flipX 속성을 변경하여 좌우 반전 효과를 적용
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    // 방어 실패
    public void GetObstacle()
    {
        StartCoroutine(DisableControlAndResetColor());
        StartCoroutine(StunPlayer());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        button1.interactable = false;
        button2.interactable = false;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
        }

        // Wait for 2 seconds
        yield return new WaitForSeconds(1.5f);

        // Change color back to white
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }

        button1.interactable = true;
        button2.interactable = true;
    }

    // 플레이어 스턴
    public IEnumerator StunPlayer()
    {
        isStunned = true;
        // 보더콜리 넘어지는 스프라이트로 1.5초 변경
        SwapAnimation();
        yield return new WaitForSeconds(1.5f);
        isStunned = false;
    }

    public void SwapAnimation()
    {
        StartCoroutine(SwapAnimationCoroutine());
    }

    private IEnumerator SwapAnimationCoroutine()
    {
        // 넘어지는 애니메이션으로 변경
        anim.SetBool("isSlide", true);
        yield return new WaitForSeconds(1.5f); // 1초 대기
        // 기존 애니메이션으로 복원
        anim.SetBool("isSlide", false);
    }
}
