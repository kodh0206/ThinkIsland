using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg3jelly : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" ) //poop2 is jelly
        {
            MiniGameManager.Instance.AddJelly();
            MiniGame3Manager.instance.AddScore();
            Destroy(this.gameObject);

        }
        else if (other.gameObject.tag == "poop" || other.gameObject.tag == "jelly")
        {
            return;
        }

        Instantiate(particle, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().Play();
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(gameObject, 1f);
    }
}
