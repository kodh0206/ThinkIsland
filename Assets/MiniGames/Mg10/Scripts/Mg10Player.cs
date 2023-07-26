using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using DG.Tweening;
public class Mg10Player : MonoBehaviour
{
    // Start is called before the first frame update
    public MMF_Player mMF_Player;
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

    public GameObject stunEffect;
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
            rb.velocity = new Vector2(rb.velocity.x, -verticalSpeed); 
            return; 
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


    public void GetHit()  
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        

        mMF_Player?.PlayFeedbacks();
        
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        Mg10Camera.instance.ShakeCamera();
        enabled = false;


        Vector2 Effectposition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        GameObject HitEff = Instantiate(stunEffect, Effectposition, Quaternion.identity, transform);
        
        yield return new WaitForSeconds(2f);

        Mg10Camera.instance.ShakeCameraEnd();

        Destroy(HitEff);
        enabled = true;

        
        

       
    }

    IEnumerator ChangeSpriteWithDelay(Sprite[] sprites, float delay , int direc)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprites[0];
        yield return new WaitForSeconds(delay);

        spriteRenderer.sprite = sprites[direc];
    }
   

}
