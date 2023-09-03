using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;


public class NumberParticle : MonoBehaviour
{
    public ParticleSystem particleEffect;
    public Sprite[] numberSprites; // 0~9까지의 스프라이트 배열
    [SerializeField]
    private SpriteRenderer tensRenderer; // 십의 자리 숫자를 위한 렌더러
    [SerializeField]
    private SpriteRenderer onesRenderer; // 일의 자리 숫자를 위한 렌더러
    [SerializeField]
    private SpriteRenderer fxRenderer; // 젤리 먹은 이펙트를 위한 렌더러
    

    private float bobbingSpeed = 1f;  // 숫자 움직임 속도
    private float bobbingAmount = 0.1f;  // 움직임 범위
    private float fadeDuration = 1f;  // 서서히 사라지는 시간

    public void DisplayNumber(int number, Vector3 position)
    {
     if (number < 0 || number > 99)
    {
        Debug.LogError("Number out of range! The number should be between 0 and 99.");
        return;
    }

    // 십의 자리와 일의 자리 숫자를 분리
    int tens = number / 10;
    int ones = number % 10;

    if (tens == 0) 
    {
        // 숫자가 한 자리일 때 십의 자리 숫자를 비활성화
        tensRenderer.enabled = false;
    }
    else 
    {
            fxRenderer.transform.position = new Vector3(position.x-0.18f,position.y,position.z);
        // 숫자가 두 자리일 때 십의 자리 숫자를 활성화 및 스프라이트 설정
        tensRenderer.enabled = true;
        tensRenderer.sprite = numberSprites[tens];
    }

    // 일의 자리 숫자 스프라이트 설정
    onesRenderer.sprite = numberSprites[ones];

    // 뒤에 생기는 Fx 켜짐
    fxRenderer.enabled = true;

    // 파티클의 위치 설정
    transform.position = position;

    // 파티클 재생
    particleEffect.Play();

    // Coroutine 시작하여 숫자가 움직이면서 서서히 사라지게 함
    StartCoroutine(BobAndFade());
    }

    private IEnumerator BobAndFade()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;

        while (elapsedTime < fadeDuration)
        {
            // 위아래로 움직임
            float newY = initialPosition.y + Mathf.Sin(elapsedTime * bobbingSpeed) * bobbingAmount;
            transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);

            // 서서히 사라지는 효과
            float alpha = 1f - (elapsedTime / fadeDuration);
            if (tensRenderer.enabled)
            {
                Color tensColor = tensRenderer.color;
                tensRenderer.color = new Color(tensColor.r, tensColor.g, tensColor.b, alpha);
            }

            Color onesColor = onesRenderer.color;
            onesRenderer.color = new Color(onesColor.r, onesColor.g, onesColor.b, alpha);
            
            Color fxColor = fxRenderer.color;
            fxRenderer.color = new Color(fxColor.r, fxColor.g, fxColor.b, alpha);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 파티클 지속 시간 후 삭제
        Destroy(gameObject);
    }
}