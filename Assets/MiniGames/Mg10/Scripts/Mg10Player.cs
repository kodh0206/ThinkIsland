using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float initialSpeed = 5.0f;
    public float horizontalSpeed = 10.0f;
    private float verticalSpeed;

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private bool isInputEnabled = true;

    public bool RightButton = false;
    public bool LeftButton = false;

    public Sprite[] sprites = new Sprite[3];
    //public Sprite sprite;
    public AudioClip skiing;
    
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
        verticalSpeed = initialSpeed;
        audioSource =GetComponent<AudioSource>();

    }

    private void Update()
    {
        
        if (!isInputEnabled)
        {
            rb.velocity = new Vector2(rb.velocity.x, -verticalSpeed); // �Ʒ��� ������
            return; // �Է� ��Ȱ��ȭ ���¸� ������Ʈ ����
        }

        // ����Ű �Է� ó��
        if (Input.GetKeyDown(KeyCode.LeftArrow) || LeftButton)
        {
            StartCoroutine(ChangeSpriteWithDelay(sprites, 0.1f,1));
            rb.velocity = new Vector2(-horizontalSpeed, rb.velocity.y);
            
        }
        // ������Ű �Է� ó��
        else if (Input.GetKeyDown(KeyCode.RightArrow) ||RightButton)
        {
            StartCoroutine(ChangeSpriteWithDelay(sprites, 0.1f,2));
            rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
        }

        // �Է��� ���� ��
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, -verticalSpeed);
        }
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

    IEnumerator ChangeSpriteWithDelay(Sprite[] sprites, float delay , int direc)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprites[0];
        yield return new WaitForSeconds(delay);

        spriteRenderer.sprite = sprites[direc];
    }


}
