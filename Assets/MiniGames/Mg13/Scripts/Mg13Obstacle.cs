using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg13Obstacle : MonoBehaviour
{
    public float obstacleSpeed = 5f; 
    private Transform target; 
    private bool getTarget = false;
    public Vector2 direction;

    private void Start()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if ((player != null) && !(getTarget))
        {
            target = player.transform;
            getTarget = true;
            direction = (target.position - transform.position).normalized;

        }
    }

    private void Update()
    {
        if (target != null)
        {
            
            transform.Translate(direction * obstacleSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 Hittarget = other.transform.position;
            Vector2 Knuckdirection = new Vector2(Hittarget.x - transform.position.x, Hittarget.y - transform.position.y).normalized;
            float pushDistance = 0.5f; // 플레이어를 밀어낼 거리

            other.transform.Translate(Knuckdirection * pushDistance, Space.World);
            
            Mg13manager.instance.GameLevelDown();
            other.gameObject.GetComponent<Mg13Player>().GetHit();
        }
    }

    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}
