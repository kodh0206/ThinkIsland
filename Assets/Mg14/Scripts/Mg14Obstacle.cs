using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg14Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Mg14Player>().GetHit();
        }

    }
}
