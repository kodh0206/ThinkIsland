using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10Obstacle : MonoBehaviour
{

    public float obstacleSpeed = 10.0f;


    private Rigidbody2D rb;
    private bool isInputEnabled = true;

    private void Start()
    {

    }

    private void Update()
    {
        transform.position += Vector3.up * obstacleSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   

            Mg10Player player = other.gameObject.GetComponent<Mg10Player>();
            Mg10manager.instance.GameLevelDown();

            if (player != null)
            {   
                //AudioManager.Instance.Rock();
                player.GetHit();
            }

            Mg10Spawner spawner1 = FindAnyObjectByType<Mg10Spawner>();
            spawner1.GetHit();
            Mg10jellySpawner spawner2 = FindAnyObjectByType<Mg10jellySpawner>();
            spawner2.GetHit();

        }
        


    }



    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}
