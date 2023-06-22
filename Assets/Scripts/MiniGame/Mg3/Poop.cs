using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{

    [SerializeField]
    private GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter2D(Collision2D other) {
    //    Debug.Log("On Collision");
    //}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Ground" && this.gameObject.tag == "poop")
        {
            
            
        } else if (other.gameObject.tag == "Player" && this.gameObject.tag =="poop")
        {
            GameManager.instance.SetGameOver();
            other.gameObject.GetComponent<Player>().GetPoop();
        }
        else if (other.gameObject.tag == "Player" && this.gameObject.tag == "poop2") //poop2 is jelly
        {
            GameManager.instance.AddScore();
        }

        else if (other.gameObject.tag == "poop" || other.gameObject.tag == "poop2")
        {
            return;
        }
        Instantiate(particle,transform.position, Quaternion.identity);
        GetComponent<AudioSource>().Play();
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 1f);

    }
}
