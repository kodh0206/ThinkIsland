using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Ground" && this.gameObject.tag == "poop")
        {
            
        } 
        else if (other.gameObject.tag == "Player" && this.gameObject.tag =="poop")
        {   
            AudioManager.Instance.PlayPoop();
            MiniGame3Manager.instance.StunPlayer();
            other.gameObject.GetComponent<Player>().GetPoop();
        }
        else if (other.gameObject.tag == "Player" && this.gameObject.tag == "poop2") //poop2 is jelly
        {
            MiniGameManager.Instance.AddJelly();
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