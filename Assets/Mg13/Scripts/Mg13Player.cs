using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg13Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 180.0f;
    private float currentRotation;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            // �Է��� �ִ� ��� ȸ��
            currentRotation -= horizontalInput * rotateSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0f, 0f, currentRotation);
        }

        // ������
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
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
