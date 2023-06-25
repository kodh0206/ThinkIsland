using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1Cow : MonoBehaviour
{
    [SerializeField]
    private float cowSpeed = 5.0f;

    void Update()
    {
        transform.position += Vector3.left * cowSpeed * Time.deltaTime;
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
        cowSpeed = speed;
    }
}
