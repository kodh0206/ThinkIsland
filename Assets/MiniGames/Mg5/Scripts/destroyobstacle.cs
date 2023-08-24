using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyobstacle : MonoBehaviour
{
    // Start is called before the first frame update
    
    private bool obstacleEntered = false;
    private Rigidbody2D playerRigidbody;

    public GameObject Mg5obstaclePrefab; 

    void Start()
    {
        
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("obstacle"))
        {
            ResetPlayerPosition();
        }
    }

    public void  ResetPlayerPosition()
    {
        
        GameObject obstacle = GameObject.FindGameObjectWithTag("obstacle");
        Destroy(obstacle);

    }
}
