using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg12Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Animator animator;
    [SerializeField]
    private float moveSpeed = 5f;

    private bool moveUp = false;
        private bool moveDown = false;



    
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            moveUp = true;
            moveDown = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            moveUp = false;
            moveDown = true;
        }

        if (moveUp)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        else if (moveDown)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }

    }

    IEnumerator shootCoroutine;

    public void PlayShootAnimation()
    {
        // 이전에 실행중인 Coroutine이 있다면 중지
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
        }

        // 새로운 Coroutine을 시작
        shootCoroutine = ShootAnimationRoutine();
        StartCoroutine(shootCoroutine);
    }

    private IEnumerator ShootAnimationRoutine()
    {
        animator.SetBool("Shoot", true);

        yield return new WaitForSeconds(0.2f);

        animator.SetBool("Shoot", false);
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
