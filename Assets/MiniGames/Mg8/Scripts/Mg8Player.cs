using DG.Tweening;
using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Mg8Player : MonoBehaviour
{
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
        // 움직임 멈춤
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        ShakeCamera();

        // 비동기 처리 시작
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // 조작 비활성화
        enabled = false;

        // 색상 변경
        Vector2 Effectposition = new Vector2(transform.position.x+0.2f, transform.position.y + 1.4f);
        GameObject HitEff = Instantiate(stunEffect, Effectposition, Quaternion.identity, transform);
        Destroy(HitEff, 1.5f);

        // 2초간 대기
        yield return new WaitForSeconds(1.5f);

        // 조작 활성화
        enabled = true;

        // 1초간 poop 영향 받지 않음

        // 색상 원래대로 복구
        
    }


    public int MushroomCount()
    {
        return Mushrooms;
    }

    public void ShakeCamera()
    {
        Camera.main.transform.DOShakePosition(2f, 0.2f, 40);  // 카메라를 1초 동안, 강도 0.4로 20번 흔듭니다.
    }

}
