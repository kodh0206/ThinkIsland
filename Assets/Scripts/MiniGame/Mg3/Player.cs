using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private float moveSpeed = 5f;

    public int level;

    public Player()
    {
        level = 1;
    }


    private bool RightButton = false;
    private bool LeftButton = false;

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
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (MiniGame3Manager.instance.isStunned == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || LeftButton)
            {
                Vector3 currScale = transform.localScale;
                transform.localScale = new Vector3(-Mathf.Abs(currScale.x), currScale.y, currScale.z);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                animator.SetBool("isRunning", true);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || RightButton)
            {
                Vector3 currScale = transform.localScale;
                transform.localScale = new Vector3(Mathf.Abs(currScale.x), currScale.y, currScale.z);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    // When the player gets hit by poop, call this function
    public void GetPoop()
    {
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // Change color to brown
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
        }

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Change color back to white
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
