using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using DG.Tweening;
public class Mg10Player : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    
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


    public float blinkInterval = 0.125f; //blink
    public float minAlpha = 0.3f; // 최소 알파값 (반투명 상태)
    public float maxAlpha = 1f;   // 최대 알파값 (불투명 상태)

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        verticalSpeed = initialSpeed;
        audioSource =GetComponent<AudioSource>();

    }

    private void Update()
    {   bool isSFXOn = AudioManager.Instance.isSFXOn;

    // 만약 isSFXOn이 false이면 모든 오디오를 멈춤
        if (!isSFXOn && audioSource.isPlaying)
        {
            audioSource.Stop();
            return; // 추가적인 오디오 재생 명령을 건너뜀
        }
        
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

        for (int i = 0; i < 8; i++) //Blink
        {
            Blink();
            yield return new WaitForSeconds(blinkInterval);
            BlinkEnd();
            yield return new WaitForSeconds(blinkInterval);
        }

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

    public void Blink()
    {
        spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                minAlpha); // 반투명 상태로 설정

    }

    public void BlinkEnd()
    {
        spriteRenderer.color = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            maxAlpha); // 불투명 상태로 설정
    }
}
