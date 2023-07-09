using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10backgroundmoving : MonoBehaviour
{
    public float initialSpeed = 5.0f;
    public float horizontalSpeed = 10.0f;
    private float verticalSpeed;

    private Rigidbody2D rb;
    private bool isInputEnabled = true; // �Է� Ȱ��ȭ ����

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        verticalSpeed = initialSpeed;
    }

    private void Update()
    {
        // �Է� Ȱ��ȭ�Ǿ� ���� ���� �Է� ó��
        if (isInputEnabled)
        {
            // ����Ű �Է� ó��
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-horizontalSpeed, rb.velocity.y);
            }
            // ������Ű �Է� ó��
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
            }

            // �Է��� ���� ��
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(0f, verticalSpeed);
            }
        }
    }

    public void GetHit()
    {
        // ������ ����
        rb.velocity = new Vector2(0f, verticalSpeed);

        // �Է� ��Ȱ��ȭ
        isInputEnabled = false;

        // �񵿱� ó�� ����
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // 2�ʰ� ���
        yield return new WaitForSeconds(2f);

        // �Է� Ȱ��ȭ
        isInputEnabled = true;

        // 1�ʰ� �Է� ���� ���� ����
        yield return new WaitForSeconds(1f);
    }
}
