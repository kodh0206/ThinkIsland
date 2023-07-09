using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg11Obstacle : MonoBehaviour
{


    public float obstacleSpeed = 5f; // 움직임 속도

    void Start()
    {

    }

    private void Update()
    {
        Vector2 direction = -transform.position.normalized; // (0, 0)으로 향하는 방향

        // 움직임 적용
        transform.Translate(direction * obstacleSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        else if (other.gameObject.tag == "egg")
        {
            Mg11Player player = FindAnyObjectByType<Mg11Player>();
            if (player != null)
            {
                player.GetHit();
            }

            Mg11Spawner spawner1 = FindAnyObjectByType<Mg11Spawner>();
            spawner1.GetHit();
            Mg11jellySpawner spawner2 = FindAnyObjectByType<Mg11jellySpawner>();
            spawner2.GetHit();

            Destroy(gameObject);
        }
    }



    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}
