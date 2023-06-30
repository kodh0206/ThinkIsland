using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg15Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 움직임 속도

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

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
