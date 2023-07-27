using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg12Player : MonoBehaviour
{
    public GameObject stunEffect;

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

    ShakeCamera();

    audioSource.loop = false;
    audioSource.Stop();

    StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {

        enabled = false;


        Vector2 Effectposition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        GameObject HitEff = Instantiate(stunEffect, Effectposition, Quaternion.identity, transform);


        yield return new WaitForSeconds(2f);

 
        enabled = true;
        Destroy(HitEff);

        yield return new WaitForSeconds(1f);


        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.clip = swimming;
            audioSource.Play();
        }

    }

    public void ShakeCamera()
    {
        Camera.main.transform.DOShakePosition(1.0f, 0.6f, 10);  // ī�޶� 1�� ����, ���� 0.4�� 20�� ���ϴ�.
    }

}
