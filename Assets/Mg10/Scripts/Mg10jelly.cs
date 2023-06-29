using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10jelly : MonoBehaviour
{
    public float jellySpeed = 5.0f;


    private Rigidbody2D rb;
    private bool isInputEnabled = true;

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
            Destroy(gameObject);
        }
    }


    

    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }


}
