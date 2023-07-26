using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg13Player : MonoBehaviour
{
    public GameObject stunEffect;
    public GameObject hitEff;

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 180.0f;
    private float currentRotation;


    private bool RightButton = false;
    private bool LeftButton = false;

    private AudioSource audioSource;
    public AudioClip swimming;
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
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // AudioSource 컴포넌트가 없으면 추가
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        Vector2 EffectPosition = new Vector2(transform.position.x, transform.position.y - 0.7f);
        hitEff = Instantiate(stunEffect, EffectPosition, Quaternion.identity, transform);

        // Make the HitEff invisible initially.
        SetHitEffVisibility(hitEff, false);
    }

    private void Update()
    {
        

        if (Input.GetKey(KeyCode.RightArrow) || RightButton)
        {
            
            float rotationAmount = rotateSpeed * Time.deltaTime;
            currentRotation -= rotationAmount;
            transform.eulerAngles = new Vector3(0f, 0f, currentRotation);
        }

        else if (Input.GetKey(KeyCode.LeftArrow) || LeftButton)
        {
            
            float rotationAmount = rotateSpeed * Time.deltaTime;
            currentRotation += rotationAmount;
            transform.eulerAngles = new Vector3(0f, 0f, currentRotation);
        }

        
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        if (enabled && !audioSource.isPlaying)
        {
            audioSource.clip = swimming;
            audioSource.loop = true;
            audioSource.Play();
        }
        // 캐릭터가 disabled 상태이면 소리를 멈춤
        else if (!enabled)
        {
            audioSource.Stop();
        }
    }

    public void GetHit()
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        audioSource.Stop(); // 물장구 소리를 멈춤

       

        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        
        enabled = false;


        SetHitEffVisibility(hitEff, true);
        yield return new WaitForSeconds(2f);



        
        enabled = true;

        SetHitEffVisibility(hitEff, false);
        yield return new WaitForSeconds(1f);




        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.clip = swimming;
            audioSource.Play();
        }
    }
    private void SetHitEffVisibility(GameObject hitEffect, bool isVisible)
    {
            SpriteRenderer hitEffRenderer = hitEffect.GetComponent<SpriteRenderer>();
            if (hitEffRenderer != null)
            {
                hitEffRenderer.enabled = isVisible;
            }
        
    }

}
