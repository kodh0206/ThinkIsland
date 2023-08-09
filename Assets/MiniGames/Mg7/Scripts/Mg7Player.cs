using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg7Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float jumpPower = 5.0f;
    public int level;

    public Sprite[] sprites = new Sprite[5];

    private int direc = 2;

    private  AudioSource audioSource;
    public AudioClip jump;

    private bool RightButton = false;
    private bool LeftButton = false;

    public GameObject Teerprefabs;
    private GameObject currentEffect;
    private Coroutine makeTeerCoroutine;

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

    public Mg7Player()
    {
        level = 1;
    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // If AudioSource is not attached to the gameObject
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        Vector2 Effectposition = new Vector2(transform.position.x, transform.position.y + 0.5f);
        currentEffect = Instantiate(Teerprefabs, Effectposition, Quaternion.identity, transform);
        SetTeerEffVisibility(currentEffect,false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || LeftButton)
        {
            StartMakeTeer();
            JumpWithAngle(135f);
            if (direc >0)
            {
                direc -= 1;
                StartCoroutine(ChangeSpriteWithDelay(sprites, 0f, direc));
            }
            
            LeftButton = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)||RightButton)
        {
            StartMakeTeer();
            JumpWithAngle(45f);
            if (direc < 4)
            {
                direc += 1;
                StartCoroutine(ChangeSpriteWithDelay(sprites, 0f, direc));
            }
            RightButton = false;
        }
    }

    void JumpWithAngle(float angle)
    {   
        audioSource.PlayOneShot(jump);
        float radian = angle * Mathf.Deg2Rad;
        Vector2 jumpDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        rigidbody2D.velocity = jumpDirection * jumpPower;
    }



    IEnumerator ChangeSpriteWithDelay(Sprite[] sprites, float delay, int direc)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprites[0];
        yield return new WaitForSeconds(delay);

        spriteRenderer.sprite = sprites[direc];
    }
    void StartMakeTeer()
    {
        if (makeTeerCoroutine == null) // ���� ���� �ڷ�ƾ�� ���� ��쿡�� ����
        {
            makeTeerCoroutine = StartCoroutine(MakeTeer());
        }
    }
    IEnumerator MakeTeer()
    {
        SetTeerEffVisibility(currentEffect, true);
        yield return new WaitForSeconds(1.5f);
        SetTeerEffVisibility(currentEffect, false);

        makeTeerCoroutine = null; // �ڷ�ƾ�� ����Ǿ����� ǥ��
    }
    private void SetTeerEffVisibility(GameObject TeerEffect, bool isVisible)
    {
        SpriteRenderer hitEffRenderer = TeerEffect.GetComponent<SpriteRenderer>();
        if (hitEffRenderer != null)
        {
            hitEffRenderer.enabled = isVisible;
        }

    }

}
