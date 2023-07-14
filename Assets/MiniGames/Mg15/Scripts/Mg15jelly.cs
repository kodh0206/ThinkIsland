using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg15jelly : MonoBehaviour
{
    public float gravityScaleIncreaseRate = 1f; // gravity scale
    public float maxGravityScale = 1f; // gravity scale 

    private Rigidbody2D rb;
    private bool isScaleChanged = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = -1f; // 
    }

    private void Update()
    {
        // gravity scale
        //rb.gravityScale += gravityScaleIncreaseRate * Time.deltaTime;

        //  gravity scale
        //rb.gravityScale = Mathf.Clamp(rb.gravityScale, -1f, maxGravityScale);

        // gravity scale
        //if (rb.gravityScale >= 0f && !isScaleChanged)
        //{
           // transform.localScale = Vector3.one; 
            //isScaleChanged = true; 
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("stair"))
        {
            rb.gravityScale = 1.0f;
            transform.localScale = Vector3.one; // 
            isScaleChanged = true; // 
        }

        if (other.gameObject.CompareTag("Player"))
        {   
            MiniGameManager.Instance.AddJelly();
            Mg15manager.instance.AddScore();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        gravityScaleIncreaseRate = speed;
    }
}
