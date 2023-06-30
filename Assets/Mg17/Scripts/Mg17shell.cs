using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg17shell : MonoBehaviour
{
    public float obstacleSpeed = 5.0f;

    public GameObject jellyinShell;
    public GameObject ObstacleinRock;

    void Update()
    {
        transform.position += Vector3.down * obstacleSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Mg17Player>().GetHit();
        }

        if (other.gameObject.CompareTag("Rock"))
        {
            // 절반의 확률로 jellyinShell 생성
            float randomValue = Random.value;
            if (randomValue < 0.5f)
            {
                Vector3 shellPosition = transform.position;
                Instantiate(jellyinShell, shellPosition, Quaternion.identity);
            }
            else 
            {
                Vector3 shellPosition = transform.position;
                Instantiate(ObstacleinRock, shellPosition, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}
