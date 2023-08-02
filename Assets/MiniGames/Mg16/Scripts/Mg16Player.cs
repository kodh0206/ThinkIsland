using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Mg16Player : MonoBehaviour
{
    public GameObject stunEffect;

    public GameObject sparkEffect;


    [SerializeField]
    private float moveSpeed = 3f;

    private bool moveLeft = false;
    private bool moveRight = false;

    private AudioSource audioSource;
    public AudioClip electricity;

    private SpriteRenderer spriteRenderer;
    public float blinkInterval = 0.125f; //blink
    public float minAlpha = 0.3f; // 최소 알파값 (반투명 상태)
    public float maxAlpha = 1f;   // 최대 알파값 (불투명 상태)

    private void Start()
    {
        audioSource =GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Button1Pressed()
    {
        moveLeft = true;
        moveRight = false;
    }

    public void Button2Pressed()
    {
        moveLeft = false;
        moveRight = true;
    }

    private void Update()
    {
        if (moveLeft)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        else if (moveRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    public void IncreaseSpeed()
    {
        if (moveSpeed <= 3.9f)
        {
            moveSpeed += 0.3f;
        }
    }

    public void DecreaseSpeed()
    {
        if (moveSpeed >= 3.0f)
        {
            moveSpeed -= 0.3f;
        }
    }

    public void PlayerColorChange()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
            //StunPlayer();
        }
    }

    public void PlayerColorChangeBack()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // Change color back to white
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void GetHit()
    {
        // ������ ����

        Mg16Manager.instance.GameLevelDown();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        ShakeCamera();

        audioSource.PlayOneShot(electricity);
        
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        
        enabled = false;


        Vector2 Effectposition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        GameObject HitEff = Instantiate(stunEffect, Effectposition, Quaternion.identity, transform);

        GameObject SparkEff = Instantiate(sparkEffect, transform.position, Quaternion.identity, transform);

        for (int i = 0; i < 8; i++) //Blink
        {
            Blink();
            yield return new WaitForSeconds(blinkInterval);
            BlinkEnd();
            yield return new WaitForSeconds(blinkInterval);
        }

        Destroy(HitEff);
        Destroy(SparkEff);

        enabled = true;

    }
    public void ShakeCamera()
    {
        Camera.main.transform.DOShakePosition(1.5f, 0.2f, 40);  // 카메라를 1초 동안, 강도 0.4로 20번 흔듭니다.
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
