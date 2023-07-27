using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Mg16Player : MonoBehaviour
{
    public GameObject stunEffect;


    [SerializeField]
    private float moveSpeed = 3f;

    private bool moveLeft = false;
    private bool moveRight = false;

    private AudioSource audioSource;
    public AudioClip electricity;
    private void Start()
    {
        audioSource =GetComponent<AudioSource>();
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

        yield return new WaitForSeconds(2f);

        Destroy(HitEff);

        enabled = true;

    }
    public void ShakeCamera()
    {
        Camera.main.transform.DOShakePosition(1.5f, 0.2f, 40);  // 카메라를 1초 동안, 강도 0.4로 20번 흔듭니다.
    }

}
