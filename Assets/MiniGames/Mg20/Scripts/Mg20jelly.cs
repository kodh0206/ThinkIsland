using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20jelly : MonoBehaviour
{
    public float jellySpeed = 2.0f;


    private void Start()
    {

    }

    private void Update()
    {
        transform.position += Vector3.up * jellySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {   
            MiniGameManager.Instance.AddJelly();
            Mg20manager.instance.AddScore();
            Destroy(gameObject);
        }
    }


    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
