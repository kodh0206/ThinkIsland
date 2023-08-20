using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg9DeadZone : MonoBehaviour
{
    public Camera myCamera;
    private Vector3 initialPosition;
    private bool playerEntered = false;
    private Rigidbody2D playerRigidbody;

    public GameObject playerPrefab; 

    void Start()
    {
        initialPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerEntered = true;
            ShakeCamera();
            StartCoroutine(ResetPlayerPosition());
        }
    }

    IEnumerator ResetPlayerPosition()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Mg9manager.instance.GameLevelDown(); //levelDown

        
        Player playerComponent = player.GetComponent<Player>();
        if (playerComponent != null)
        {
            playerComponent.enabled = false;
        }

        
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero;
        }

       
        Destroy(player);
        yield return new WaitForSeconds(2f);

        if (playerEntered)
        {
           
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.transform.position = initialPosition;

           
            Player newPlayerComponent = newPlayer.GetComponent<Player>();
            if (newPlayerComponent != null)
            {
                newPlayerComponent.enabled = true;
            }

            playerEntered = false;
        }
    }



    public void ShakeCamera()
    {
        myCamera.transform.DOShakePosition(1.0f, 0.6f, 10);  
    }
}
