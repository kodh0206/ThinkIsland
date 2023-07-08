using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dMg2PlayerMove : MonoBehaviour
{
    public bool isStunned = false;
    public Button button1;
    public Button button2;

    private void Start()
    {
    }

    public void Button1Pressed()
    {
        if (transform.position.x == 0)
        {
            transform.position = new Vector2(-4.3f, 0f);
        }
        else if (transform.position.x == -4.3f)
        {
            transform.position = new Vector2(4.3f, 0f);
        }
        else if (transform.position.x == 4.3f)
        {
            transform.position = Vector2.zero;
        }
    }

    public void Button2Pressed()
    {
        if (transform.position.x == 0)
        {
            transform.position = new Vector2(4.3f, 0f);
        }
        else if (transform.position.x == -4.3f)
        {
            transform.position = Vector2.zero;
        }
        else if (transform.position.x == 4.3f)
        {
            transform.position = new Vector2(-4.3f, 0f);
        }
    }

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
    }

}
