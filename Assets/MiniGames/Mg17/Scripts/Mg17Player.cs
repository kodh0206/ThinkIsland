using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg17Player : MonoBehaviour
{
    public GameObject stunEffect;
    public Camera myCamera;

    public float moveSpeed = 1f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;
    Animator animator;
    public AudioClip shooting;
    private bool RightButton = false;
    private bool LeftButton = false;

    private bool canShoot = true;
    private float shootTimer = 0;
    private float shootInterval = 0.36f; // 0.48초에 한 번씩 사운드 재생

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
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (RightButton)
        {
            horizontalInput = 1f;
        }
        else if (LeftButton)
        {
            horizontalInput = -1f;
        }

        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rb.velocity.y);

        rb.velocity = movement;

        // 총소리 재생 로직
        if (canShoot)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                audioSource.PlayOneShot(shooting);
                shootTimer = 0;
            }
        }
    }

    public void GetHit()
    {
        animator.speed = 0f;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        
        canShoot = false;

        ShakeCamera();

        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // disable control
        enabled = false;



        Vector2 Effectposition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        GameObject HitEff = Instantiate(stunEffect, Effectposition, Quaternion.identity, transform);

        yield return new WaitForSeconds(2f);

        Destroy(HitEff);

        yield return new WaitForSeconds(1f);

        // re-enable control
        enabled = true;
        animator.Play("ShottongRmg", 0, 0);
        animator.speed = 1f;

        canShoot = true;

        
    }
    public void ShakeCamera()
    {
        myCamera.transform.DOShakePosition(1.0f, 0.6f, 10);  // 카메라를 1초 동안, 강도 0.4로 20번 흔듭니다.
    }


}