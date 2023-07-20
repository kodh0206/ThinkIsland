using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19jelly : MonoBehaviour
{
    public float jellySpeed = 2.0f;


    private void Start()
    {

    }

    private void Update()
    {
        transform.position += Vector3.down * jellySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //MiniGameManager.Instance.AddJelly();
            Mg19manager.instance.AddScore();
            Destroy(gameObject);
        }
    }


    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
