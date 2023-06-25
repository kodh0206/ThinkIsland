using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg4Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float jumpPower = 5.0f;
    // Start is called before the first frame update
    public int level;
    public Mg4Player()
    {
        level = 1;
    }

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        { 
            rigidbody2D.velocity = Vector2.up * jumpPower;
        }
        
    }
}
