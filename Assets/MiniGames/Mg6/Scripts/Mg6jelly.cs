using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg6jelly : MonoBehaviour
{
    [SerializeField]
    public float jellySpeed = 5.0f;

    public bool jellydirection = true;

    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (jellydirection)
        {
            transform.position += Vector3.right * jellySpeed * Time.deltaTime;
            
        }
        else
        {
            transform.position += Vector3.left * jellySpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            MiniGameManager.Instance.AddJelly();
            Mg6manager.instance.AddScore();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
