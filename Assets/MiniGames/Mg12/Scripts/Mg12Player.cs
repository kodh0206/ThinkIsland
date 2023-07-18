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

    private AudioSource audioSource;

    public AudioClip swimming;
    public AudioClip throwing;

    private float throwTimer = 0.1f;
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
    void Awake()
{
    anim = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
    if (audioSource == null) // If AudioSource is not attached to the gameObject
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
}

    void Update()
    {   if (Input.GetKeyDown(KeyCode.LeftArrow) || LeftButton)
    {
        moveUp = true;
        moveDown = false;
    }
    else if (Input.GetKeyDown(KeyCode.RightArrow) || RightButton)
    {
        moveUp = false;
        moveDown = true;
    }

    if (moveUp || moveDown)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.clip = swimming;
            audioSource.Play();
        }
        transform.position += (moveUp ? Vector3.up : Vector3.down) * moveSpeed * Time.deltaTime;
    }
    else
    {
        audioSource.loop = false;
        audioSource.Stop();
    }

    if (mg12RockSpawner.rockThrow)
    { throwTimer += Time.deltaTime; // Increase timer.

        // Check if it's time to play the sound.
        if (throwTimer >= 1f)
        {
            audioSource.PlayOneShot(throwing); // Play throwing sound.
            throwTimer = 0f; // Reset timer.
        }

        anim.SetTrigger("Shoot");
    }
    else
    {
        throwTimer = 0f; // Reset timer if not throwing.
    }
}
    



    public void GetHit()
    {
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    rb.velocity = Vector2.zero;

    audioSource.loop = false;
    audioSource.Stop();

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

        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.clip = swimming;
            audioSource.Play();
        }
    }
}
