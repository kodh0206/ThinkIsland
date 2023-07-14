using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg13jelly : MonoBehaviour
{
    public float jellySpeed = 5f; 
    private Transform target; 
    private bool getTarget=false;
    public Vector2 direction;

    private void Start()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if ((player != null) && !(getTarget))
        {
            target = player.transform;
            getTarget=true;
            direction = (target.position - transform.position).normalized;

        }
    }

    private void Update()
    {
        if (target != null)
        {
            
            transform.Translate(direction * jellySpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MiniGameManager.Instance.AddJelly();
            Mg13manager.instance.AddScore();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
