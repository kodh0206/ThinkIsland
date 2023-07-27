using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg15Player : MonoBehaviour
{
    public GameObject stunEffect;

    public float moveSpeed = 5f; 

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    Animator animator;

    private bool RightButton = false;
    private bool LeftButton = false;

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
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.RightArrow) || RightButton)
        {
            horizontalInput = 1f;
            animator.SetBool("MoveRight", true);
            animator.SetBool("MoveLeft", false);
        }
        else if(Input.GetKey(KeyCode.LeftArrow) || LeftButton)
        {
            
            horizontalInput = -1f;
            animator.SetBool("MoveRight", false);
            animator.SetBool("MoveLeft", true);
        }
        else
        {
            animator.SetBool("MoveRight", false);
            animator.SetBool("MoveLeft", false);
        }

            
        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rb.velocity.y);

        
        rb.velocity = movement;
    }

    public void GetHit()
    {   
        AudioManager.Instance.Rock();

        Vector2 EffectPosition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        GameObject hitEff = Instantiate(stunEffect, EffectPosition, Quaternion.identity, transform);

        boxCollider.enabled = false;

        ShakeCamera();


        StartCoroutine(EnableBoxColliderAfterDelay(0.5f));
    }

    private IEnumerator EnableBoxColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        boxCollider.enabled = true;
    }


    public void ShakeCamera()
    {
        Camera.main.transform.DOShakePosition(1.5f, 0.6f, 20);  // 카메라를 1초 동안, 강도 0.4로 20번 흔듭니다.
    }

}
