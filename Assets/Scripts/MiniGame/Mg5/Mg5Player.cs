using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg5Player : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2D;

    [SerializeField]
    private float moveSpeed = 5f;
    // Start is called before the first frame update
    public int level;
    public Mg5Player()
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
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Vector3 currScale = transform.localScale;
            transform.localScale = new Vector3(-Mathf.Abs(currScale.x), currScale.y, currScale.z);
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Vector3 currScale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(currScale.x), currScale.y, currScale.z);
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            
        }

    }
}
