using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mg16Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    //public bool isStunned = false;
    //public Button button1;
    //public Button button2;

    [SerializeField]
    private float moveSpeed = 5f;

    private bool moveLeft = false;
    private bool moveRight = false;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
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

    private void Update() {
        if (moveLeft)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        else if (moveRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

/*
    // 방어 실패
    public void GetObstacle()
    {
        StartCoroutine(DisableControlAndResetColor());
        StartCoroutine(StunPlayer());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        button1.interactable = false;
        button2.interactable = false;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
        }

        // Wait for 2 seconds
        yield return new WaitForSeconds(1.5f);

        // Change color back to white
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }

        button1.interactable = true;
        button2.interactable = true;
    }


    // 플레이어 스턴
    public IEnumerator StunPlayer()
    {
        isStunned = true;

        yield return new WaitForSeconds(1.5f);
        isStunned = false;
    }*/

    

}
