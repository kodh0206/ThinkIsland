using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg11Player : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject stunEffect;
    public GameObject hitEff;

    public Camera myCamera;

    public float speed = 2.0f; // ������ �ӵ�
    public float radius = 2.0f; // ���� ������
    public float startAngle = 0.0f; // ���� ����
    public float angleRange = 90.0f; // ��� ���� ����

    private float angle = 0.0f; // ���� ����

    private float rotationAngle = 0f;
    private float lastAngle = 0f;

    private bool clock = false;
    private bool unclock=false;

    private bool RightButton = false;
    private bool LeftButton = false;

    private SpriteRenderer spriteRenderer;

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
        // ���� ���� ����
        angle = startAngle;

        spriteRenderer = GetComponent<SpriteRenderer>();

        Vector2 EffectPosition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        hitEff = Instantiate(stunEffect, EffectPosition, Quaternion.identity, transform);

        // Make the HitEff invisible initially.
        SetHitEffVisibility(hitEff, false);
    }

    private void Update()
    {
        // ������ ��ư �Է� ó��
        if (Input.GetKey(KeyCode.RightArrow) || RightButton)
        {
            unclock = false;
            clock = true;
           
        }
        // ���� ��ư �Է� ó��
        else if (Input.GetKey(KeyCode.LeftArrow) || LeftButton)
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

        // ��������Ʈ ȸ��
        float rotationAngle = -angle;// �ð���� �Ǵ� �ݽð�������� ȸ���� ����
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);
        transform.rotation = rotation;
    }

    public void GetHit()  //�¾�����
    {
        // ������ ����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        ShakeCamera();

        // �񵿱� ó�� ����
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // ���� ��Ȱ��ȭ
        enabled = false;

        // ���� ����


        SetHitEffVisibility(hitEff, true);
        

        Mg11egg.instance.EggBreak();


        yield return new WaitForSeconds(2f);

        SetHitEffVisibility(hitEff, false);

        Mg11egg.instance.EggBreakEnd();

        

        // ���� Ȱ��ȭ
        enabled = true;

        
    }


    private void SetHitEffVisibility(GameObject hitEffect, bool isVisible)
    {
        SpriteRenderer hitEffRenderer = hitEffect.GetComponent<SpriteRenderer>();
        if (hitEffRenderer != null)
        {
            hitEffRenderer.enabled = isVisible;
        }

    }



    public void ShakeCamera()
    {
        myCamera.transform.DOShakePosition(1.5f, 0.2f, 10);  // ī�޶� 1�� ����, ���� 0.4�� 20�� ���ϴ�.
    }

}
