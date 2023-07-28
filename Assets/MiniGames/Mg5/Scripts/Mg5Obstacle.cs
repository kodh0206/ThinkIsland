using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg5Obstacle : MonoBehaviour
{
    [SerializeField]
    private float obstacleSpeed = 2.0f;
    private Vector3 moveDirection;

    private bool isBreaking = false;

    private SpriteRenderer spriteRenderer;

    private float fadeDuration = 1.5f;
    private float fadeTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        // ·£´ýÇÑ ¹æÇâ º¤ÅÍ »ý¼º
        moveDirection = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0, 0.8f), 0f).normalized;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * obstacleSpeed * Time.deltaTime;

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

    }

    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isBreaking)
        {
            Invoke("BreakObject", 0.5f);
        }
    }

    private void BreakObject()
    {

        isBreaking = true;
    }

}
