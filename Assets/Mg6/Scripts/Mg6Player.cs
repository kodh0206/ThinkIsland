using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg6Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 3.0f; // �ʱ� ���� ��
    [SerializeField]
    private float maxJumpForce = 15.0f; // �ִ� ���� ��
    [SerializeField]
    private float jumpForceIncrement = 5.0f; // ���� �� ������

    [SerializeField]
    public int level=1;

    private bool isJumping = false; // ���� ������ ���� üũ
    private bool nowJumping = false;
    private float currentJumpForce = 0.0f; // ���� ���� ��

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping )
        {
            isJumping = true;
            currentJumpForce = jumpForce;
        }
        else if (Input.GetKey(KeyCode.Space) && isJumping )
        {
            currentJumpForce += jumpForceIncrement * Time.deltaTime;
            currentJumpForce = Mathf.Clamp(currentJumpForce, 0.0f, maxJumpForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isJumping && !nowJumping)
        {
            Jump();
            nowJumping = true;
            isJumping = false;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, currentJumpForce);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            nowJumping = false;
        }
    }

    public void GetHit()
    {
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
