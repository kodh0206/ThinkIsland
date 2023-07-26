using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20Brokenblock : MonoBehaviour
{
    public Animator animator;
    public float blockspeed = 3f;

    private SpriteRenderer spriteRenderer;
    private bool isBreaking = false;
    private float fadeDuration = 1.0f;
    private float fadeTimer = 0.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isBreaking)
        {
            Color currentColor = spriteRenderer.color;
            fadeTimer += Time.deltaTime;
            float t = Mathf.Clamp01(fadeTimer / fadeDuration);
            float newAlpha = Mathf.Lerp(currentColor.a, 0f, t);
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

            if (spriteRenderer.color.a <= 0.01f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(Vector3.up * blockspeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isBreaking)
        {
            animator.SetBool("Break", true);
            Invoke("BreakObject", 0.5f); 
        }
    }

    private void BreakObject()
    {
        
        isBreaking = true;
    }
    public void SetSpeed(float speed)
    {
        blockspeed = speed;
    }
}



