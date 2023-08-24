using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private Animator animator;

    public GameObject stunEffect;

    public Camera myCamera;

    private bool LeftMovig=false;

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
                LeftMovig = true;
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                animator.SetBool("isRunning", false);
                animator.SetBool("isLeftRunning", true);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || RightButton)
            {
                LeftMovig = false;
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                animator.SetBool("isLeftRunning", false);
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isLeftRunning", false);
                if (LeftMovig)
                {
                    animator.SetBool("IdleLeft", true);
                }
                else
                {
                    animator.SetBool("IdleLeft", false);
                }
                
            }

        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isLeftRunning", false);
        }
    }

    // When the player gets hit by poop, call this function
    public void GetPoop(float Pos)
    {
        StartCoroutine(DisableControlAndResetColor(Pos));
    }

    private IEnumerator DisableControlAndResetColor(float Pos)
    {

        // Change color to brown
        

        

        
        if (Pos <= 0) //RightHIt
        {
            animator.SetBool("ISLeftHit", true);
            Vector2 target = new Vector2(transform.position.x - 0.4f, transform.position.y);
            transform.DOMove(target, 0.2f);
        }
        else
        {
            animator.SetBool("ISHIt", true);

            Vector2 target = new Vector2(transform.position.x + 0.4f, transform.position.y);
            transform.DOMove(target, 0.2f);
            
        }

        ShakeCamera();

        Vector2 Effectposition = new Vector2(transform.position.x, transform.position.y + 0.7f);
        GameObject HitEff = Instantiate(stunEffect, Effectposition, Quaternion.identity, transform);



        // Wait for 2 seconds
        yield return new WaitForSeconds(1f);

        Destroy(HitEff);


        animator.SetBool("ISHIt", false);

        animator.SetBool("ISLeftHit", false);
        // Change color back to white

    }

    public void ShakeCamera()
    {
        myCamera.transform.DOShakePosition(1, 0.5f);  
    }


}
