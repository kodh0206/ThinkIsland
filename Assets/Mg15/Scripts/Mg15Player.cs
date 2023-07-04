using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg15Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 움직임 속도

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;


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
    }

    private void Update()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.RightArrow) || RightButton)
        {
            horizontalInput = 1f;
        }
        else if(Input.GetKey(KeyCode.LeftArrow) || LeftButton)
        {
            horizontalInput = -1f;
        }

            // 움직임 계산
        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rb.velocity.y);

        // 움직임 적용
        rb.velocity = movement;
    }

    public void GetHit()
    {
        // BoxCollider2D를 비활성화
        boxCollider.enabled = false;

        // 0.5초 후에 BoxCollider2D를 활성화
        StartCoroutine(EnableBoxColliderAfterDelay(0.5f));
    }

    private IEnumerator EnableBoxColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        boxCollider.enabled = true;
    }

}
