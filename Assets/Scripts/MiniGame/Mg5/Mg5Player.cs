using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mg5Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Animator animator;

    [SerializeField]
    private float moveSpeed = 5f;

    private bool RightButton = false;
    private bool LeftButton = false;


    

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

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

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || LeftButton)
        {
            animator.SetBool("Quokkastand", false);
            animator.SetBool("QuokkaRightRun", false);
            animator.SetBool("QuokkaLeftRun", true);

            Vector3 currScale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(currScale.x), currScale.y, currScale.z);
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || RightButton)

        {
            animator.SetBool("Quokkastand", false);
            animator.SetBool("QuokkaLeftRun", false);
            animator.SetBool("QuokkaRightRun", true);
            Vector3 currScale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(currScale.x), currScale.y, currScale.z);
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        else
        {
            animator.SetBool("QuokkaLeftRun", false);
            animator.SetBool("QuokkaRightRun", false);
            animator.SetBool("Quokkastand", true);
        }
        
       
    }

}
