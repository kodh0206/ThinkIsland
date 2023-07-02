using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg18Obstacle : MonoBehaviour
{
    public float ObstacleSpeed = 5.0f;




    private void Start()
    {

    }

    private void Update()
    {
        transform.position += Vector3.left * ObstacleSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Mg18Player>().GetHit();
        }

        if (other.gameObject.CompareTag("jelly"))
        {
            Destroy(gameObject);
        }
    }


    public void SetSpeed(float speed)
    {
        ObstacleSpeed = speed;
    }
}
