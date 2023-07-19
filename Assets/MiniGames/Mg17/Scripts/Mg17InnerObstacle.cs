using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg17InnerObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    public float obstacleSpeed = 5.0f;


    private Rigidbody2D rb;
    private bool isInputEnabled = true;

    private void Start()
    {

    }

    private void Update()
    {
        transform.position += Vector3.down * obstacleSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {   
            AudioManager.Instance.PlayPoop();
            other.gameObject.GetComponent<Mg17Player>().GetHit();
            Destroy(gameObject);
            Mg17Spawner spawner1 = FindObjectOfType<Mg17Spawner>();
            spawner1.GetHit();
            Mg17RockSpawner spawner2 = FindObjectOfType<Mg17RockSpawner>();
            spawner2.GetHit();
        }
    }


    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}
