using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;

    public float otherPos;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            
        } 
        else if (other.gameObject.tag == "Player")
        {
            //AudioManager.Instance.PlayPoop();
            otherPos=other.transform.position.x-transform.position.x;
            MiniGame3Manager.instance.StunPlayer();
            MiniGame3Manager.instance.GameLevelDown(); // Hit and level Down
            other.gameObject.GetComponent<Player>().GetPoop(otherPos);
        }
        
        else if (other.gameObject.tag == "poop" || other.gameObject.tag == "jelly")
        {
            return;
        }
        
        Instantiate(particle,transform.position, Quaternion.identity);
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