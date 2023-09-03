using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg13Player : MonoBehaviour
{

    public Camera myCamera;
    public GameObject stunEffect;
    public GameObject hitEff;
    public SpriteRenderer spriteRenderer;

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 260.0f;
    private float currentRotation;

    public float blinkInterval = 0.125f; //blink
    public float minAlpha = 0.3f; // 최소 알파값 (반투명 상태)
    public float maxAlpha = 1f;   // 최대 알파값 (불투명 상태)


    private bool RightButton = false;
    private bool LeftButton = false;

    private AudioSource audioSource;
    public AudioClip swimming;
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
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
     void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // AudioSource 컴포넌트가 없으면 추가
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        Vector2 EffectPosition = new Vector2(transform.position.x, transform.position.y - 0.7f);
        hitEff = Instantiate(stunEffect, EffectPosition, Quaternion.identity, transform);

        // Make the HitEff invisible initially.
        SetHitEffVisibility(hitEff, false);
    }

    private void Update()
    {
        

        if (Input.GetKey(KeyCode.RightArrow) || RightButton)
        {
            
            float rotationAmount = rotateSpeed * Time.deltaTime;
            currentRotation -= rotationAmount;
            transform.eulerAngles = new Vector3(0f, 0f, currentRotation);
        }

        else if (Input.GetKey(KeyCode.LeftArrow) || LeftButton)
        {
            
            float rotationAmount = rotateSpeed * Time.deltaTime;
            currentRotation += rotationAmount;
            transform.eulerAngles = new Vector3(0f, 0f, currentRotation);
        }

        
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        if (enabled && !audioSource.isPlaying&& AudioManager.Instance.isSFXOn)
        {
            audioSource.clip = swimming;
            audioSource.loop = true;
            audioSource.Play();
        }
        // 캐릭터가 disabled 상태이면 소리를 멈춤
        else if (!enabled)
        {
            audioSource.Stop();
        }
    }

    public void GetHit()
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        audioSource.Stop(); // 물장구 소리를 멈춤

        GameObject[] ObstacleObjects = GameObject.FindGameObjectsWithTag("Obstacle"); //필드 파괴
        foreach (var ObstacleObject in ObstacleObjects)
        {
            Destroy(ObstacleObject);
        }
        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //필드 젤리 파괴
        foreach (var jellyObject in jellyObjects)
        {
            Destroy(jellyObject);
        }
        RotateOneCircle();

        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        
        enabled = false;
        ShakeCamera();

        

        SetHitEffVisibility(hitEff, true);

        for (int i = 0; i < 8; i++) //Blink
        {
            Blink();
            yield return new WaitForSeconds(blinkInterval);
            BlinkEnd();
            yield return new WaitForSeconds(blinkInterval);
        }


        enabled = true;

        SetHitEffVisibility(hitEff, false);
        yield return new WaitForSeconds(1f);




        if (!audioSource.isPlaying &&AudioManager.Instance.isSFXOn)
        {
            audioSource.loop = true;
            audioSource.clip = swimming;
            audioSource.Play();
        }
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
        Vibration.Instance.Vibrate();
        myCamera.transform.DOShakePosition(1.5f, 0.6f, 15);  // 카메라를 1초 동안, 강도 0.4로 20번 흔듭니다.
    }



    private void RotateOneCircle()
    {
        // DOTween을 사용하여 오브젝트를 한 바퀴 회전시킵니다.
        transform.DORotate(new Vector3(0f, 0f, 360f), 1.5f, RotateMode.LocalAxisAdd)
            .SetEase(Ease.OutQuint); // 회전에 사용할 움직임(Ease)을 설정합니다.
    }

    public void Blink()
    {
        spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                minAlpha); // 반투명 상태로 설정
    }

    public void BlinkEnd()
    {
        spriteRenderer.color = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            maxAlpha); // 불투명 상태로 설정
    }

}
