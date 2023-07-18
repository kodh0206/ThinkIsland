using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg14jelly : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MiniGameManager.Instance.AddJelly();
            Mg14manager.instance.AddScore();
            Destroy(gameObject);
        }
    }
}
