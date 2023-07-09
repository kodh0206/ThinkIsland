using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg8Obstacle : MonoBehaviour
{
    [SerializeField]
    public float obstacleSpeed = 0f;
    // Start is called before the first frame update

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

            transform.position += Vector3.left * obstacleSpeed * Time.deltaTime;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Mg8Player>().GetHit();
            animator.SetBool("PlayerHit", true);
            
        }
        
    }
    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}
