using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mg16Player : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 3f;

    private bool moveLeft = false;
    private bool moveRight = false;

    private void Start()
    {
    }

    public void Button1Pressed()
    {
        moveLeft = true;
        moveRight = false;
    }

    public void Button2Pressed()
    {
        moveLeft = false;
        moveRight = true;
    }

    private void Update()
    {
        if (moveLeft)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        else if (moveRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    public void SpeedTime()
    {
        if (moveSpeed <= 3.9f)
        {
            moveSpeed += 0.3f;
        }
    }

    public void PlayerColorChange()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
            //StunPlayer();
        }
    }

    public void PlayerColorChangeBack()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // Change color back to white
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
