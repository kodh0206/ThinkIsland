using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2PlayerMove : MonoBehaviour
{
    public bool isStunned = false;

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
    }

    private IEnumerator DisableControlAndResetColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            //spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
            spriteRenderer.color = new Color(0f, 0f, 0f);
            //StunPlayer();
        }

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Change color back to white
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }


    public void StunPlayer()
    {
        isStunned = true;
        StartCoroutine(RecoverFromStun());
    }

    private IEnumerator RecoverFromStun()
    {
        yield return new WaitForSeconds(2f);
        isStunned = false;
    }

}
