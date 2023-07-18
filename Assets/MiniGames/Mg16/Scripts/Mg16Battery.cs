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
    public AudioSource audioSource;
    // 전기
    public GameObject electricity1;
    public GameObject electricity2;
    public GameObject electricity3;
    public GameObject electricity4;
    public GameObject electricity5;

    private SpriteRenderer spriteRenderer;

    public AudioClip dump;
    public AudioClip spalash;
    public AudioClip electricity;
    public float startY = 1.7f;
    public float topY = 2.5f;
    // 배터리 끝 위치
    public float stopY = -3.5f;
    // 해당 위치까지 이동하는 데 걸리는 시간
    public float moveDuration = 1.5f;
      private bool hasPlayedSplashSound = false;  

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();  // 애니메이터 컴포넌트 가져오기
        mg16BatterySpawner = GetComponent<Mg16BatterySpawner>();
        spriteRenderer.sprite = batterySprite;  // 초기 스프라이트를 물에 안 빠진 배터리로 설정
        audioSource =GetComponent<AudioSource>();
        // 배터리 비활성화 함수
        ElectricitySetActiveFalse();

        StartMovement();
    }

    private void Update()
    {
        if (transform.position.y < stopY+1f)
        {
            animator.SetBool("BatteryInWater", true); // 기존 애니메이션 재생 정지
            ChangeSprite();  // 스프라이트 변화 함수 호출
        }

        if (transform.position.y < stopY+0.5f)
        {
            // 전기 활성화 함수
            ElectricitySetActiveTrue();

              if (!hasPlayedSplashSound)
            {
                audioSource.PlayOneShot(spalash);
                audioSource.PlayOneShot(electricity);
                hasPlayedSplashSound = true;
            }
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
    {   audioSource.PlayOneShot(dump);
        transform.DOMoveY(stopY, moveDuration).SetEase(Ease.Linear);
       
    }

    private void StopMovement()
    {
        
    }

    void ElectricitySetActiveFalse()
    {
        electricity1.SetActive(false);
        electricity2.SetActive(false);
        electricity3.SetActive(false);
        electricity4.SetActive(false);
        electricity5.SetActive(false);
    }

    void ElectricitySetActiveTrue()
    {   
        
        electricity1.SetActive(true);
        electricity2.SetActive(true);
        electricity3.SetActive(true);
        electricity4.SetActive(true);
        electricity5.SetActive(true);
    }

    public void BatterySetBoolFalse()
    {   
        animator.SetBool("FishSetActiveFalse", false);
    }

    public void BatterySetBoolTrue()
    {   
        animator.SetBool("FishSetActiveFalse", true);
    }

}