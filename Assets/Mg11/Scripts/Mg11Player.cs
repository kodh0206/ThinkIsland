using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg11Player : MonoBehaviour
{
    // Start is called before the first frame update


    public float speed = 2.0f; // ������ �ӵ�
    public float radius = 2.0f; // ���� ������
    public float startAngle = 0.0f; // ���� ����
    public float angleRange = 90.0f; // ��� ���� ����

    private float angle = 0.0f; // ���� ����

    private bool clock = false;
    private bool unclock=false;

    private void Start()
    {
        // ���� ���� ����
        angle = startAngle;
    }

    private void Update()
    {
        // ������ ��ư �Է� ó��
        if (Input.GetKey(KeyCode.RightArrow))
        {
            unclock = false;
            clock= true;
        }
        // ���� ��ư �Է� ó��
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            clock = false;
            unclock = true;
            
        }

        if (clock)
        {
            angle += speed * Time.deltaTime;
        }
        else if (unclock)
        {
            angle -= speed * Time.deltaTime;
        }

        // ��� ���� ���� ���� ����
        angle = Mathf.Clamp(angle, startAngle - angleRange, startAngle + angleRange);

        // ��� ���
        float x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        float y = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;

        // �̵�
        transform.position = new Vector3(x, y, 0f);
    }

    public void GetHit()  //�¾�����
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