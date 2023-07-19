using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg8jelly : MonoBehaviour
{
    [SerializeField]
    private float jellySpeed = 5.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * jellySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //MiniGameManager.Instance.AddJelly();
            Mg8manager.instance.AddScore();
            
        }

        if (other.gameObject.tag == "Obstacle")
        {
            
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
