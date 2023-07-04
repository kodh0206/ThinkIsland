using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg12Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Animator anim;

    Mg12RockSpawner mg12RockSpawner;
  
    [SerializeField]
    private float moveSpeed = 5f;

    private bool moveUp = false;
    private bool moveDown = false;

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


    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        mg12RockSpawner = FindObjectOfType<Mg12RockSpawner>();
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || LeftButton)
        {
            moveUp = true;
            moveDown = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || RightButton)
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

        if (mg12RockSpawner.rockThrow)
        {
            anim.SetTrigger("Shoot");
        }

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
