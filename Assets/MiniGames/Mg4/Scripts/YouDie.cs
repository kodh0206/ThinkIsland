using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YouDie : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool playerEntered = false;
    private Rigidbody2D playerRigidbody;
    public Camera myCamera;
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

        Mg4manager.instance.achievementFail = true;
    }

    IEnumerator ResetPlayerPosition()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        
        Player playerComponent = player.GetComponent<Player>();
        if (playerComponent != null)
        {
            playerComponent.enabled = false;
        }

        
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero;
        }

        Mg4manager.instance.GameLevelDown(); // level Down




        
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
        myCamera.transform.DOShakePosition(0.8f, 1f);  
    }
}
