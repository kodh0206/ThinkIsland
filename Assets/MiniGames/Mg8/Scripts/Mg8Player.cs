using DG.Tweening;
using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Mg8Player : MonoBehaviour
{
    public Camera myCamera;
    private bool RightButton = false;
    private bool LeftButton = false;

    private int Mushrooms = 0;

    public GameObject stunEffect;


    public static Mg8Player instance = null;

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


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Mushrooms != 4) && (Input.GetKeyDown(KeyCode.X) || RightButton))
        {
            transform.position += Vector3.up * 1;
            Mushrooms += 1;
            RightButton=false;

        }
        else if ((Mushrooms != 0) && (Input.GetKeyDown(KeyCode.Z) || LeftButton))
        {
            transform.position += Vector3.down * 1;
            Mushrooms -= 1;
            LeftButton=false;
        }


    }



    public void GetHit()
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
        Vector2 Effectposition = new Vector2(transform.position.x+0.2f, transform.position.y + 1.4f);
        GameObject HitEff = Instantiate(stunEffect, Effectposition, Quaternion.identity, transform);
        Destroy(HitEff, 1.5f);

        // 2�ʰ� ���
        yield return new WaitForSeconds(1.5f);

        // ���� Ȱ��ȭ
        enabled = true;

        // 1�ʰ� poop ���� ���� ����

        // ���� ������� ����
        
    }


    public int MushroomCount()
    {
        return Mushrooms;
    }

    public void ShakeCamera()
    {
        myCamera.transform.DOShakePosition(2f, 0.2f, 40);  // ī�޶� 1�� ����, ���� 0.4�� 20�� ���ϴ�.
    }

}
