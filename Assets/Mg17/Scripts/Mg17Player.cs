using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg17Player : MonoBehaviour
{
    public float moveSpeed = 1f; // ������ �ӵ�

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
        // ������ ����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        // �񵿱� ó�� ����
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // ���� ��Ȱ��ȭ
        enabled = false;

        // ���� ����
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
        }

        // 2�ʰ� ���
        yield return new WaitForSeconds(2f);

        // ���� Ȱ��ȭ
        enabled = true;

        // 1�ʰ� poop ���� ���� ����
        yield return new WaitForSeconds(1f);

        // ���� ������� ����
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
