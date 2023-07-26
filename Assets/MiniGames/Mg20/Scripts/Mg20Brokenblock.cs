using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20Brokenblock : MonoBehaviour
{
    

    public Animator animator;

    public float blockspeed = 3f;


    private void Start()
    {
        animator = GetComponent<Animator>();

    }

        private void Update()
    {
        
        transform.Translate(Vector3.up * blockspeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            BreakObject();
        }
    }

    private void BreakObject()
    {   //AudioManager.Instance.BreakPlatform();

        animator.SetBool("Break", true);

        
        Invoke("DestroyObject", 0.5f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void SetSpeed(float speed)
    {
        blockspeed = speed;
    }
}
