using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg17Rock : MonoBehaviour
{
    // Start is called before the first frame update
    public float RockSpeed = 5.0f;



    void Update()
    {
        transform.position += Vector3.up * RockSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        RockSpeed = speed;
    }
}
