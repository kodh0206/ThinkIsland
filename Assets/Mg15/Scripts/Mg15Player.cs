using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg15Player : MonoBehaviour
{
    public float moveSpeed = 5f; // ������ �ӵ�

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

        // ������ ���
        float moveX = horizontalInput * moveSpeed;
        Vector2 movement = new Vector2(moveX, rb.velocity.y);

        // ������ ����
        rb.velocity = movement;
    }

    public void GetHit()
    {
        // BoxCollider2D�� ��Ȱ��ȭ
        boxCollider.enabled = false;

        // 0.5�� �Ŀ� BoxCollider2D�� Ȱ��ȭ
        StartCoroutine(EnableBoxColliderAfterDelay(0.5f));
    }

    private IEnumerator EnableBoxColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        boxCollider.enabled = true;
    }

}
