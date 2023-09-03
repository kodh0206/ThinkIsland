using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg11Obstacle : MonoBehaviour
{

    Vector2 direction;
    SpriteRenderer spriteRenderer;

    public float obstacleSpeed = 5f; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        direction = -transform.position.normalized; 

        
        transform.Translate(direction * obstacleSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Destroy(gameObject);
            obstacleSpeed = -obstacleSpeed;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        else if (other.gameObject.tag == "egg")
        {   
            AudioManager.Instance.PlayEggBreak();
            Mg11manager.instance.GameLevelDown();

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

            Mg11manager.instance.achievementFail = true;
        }
    }



    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}
