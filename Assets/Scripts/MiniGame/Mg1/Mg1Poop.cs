using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1Poop : MonoBehaviour
{
    [SerializeField]
    private float poopSpeed = 5.0f;

    void Update()
    {
        transform.position += Vector3.left * poopSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Mg1Manager.instance.AddScore();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        poopSpeed = speed;
    }
}
