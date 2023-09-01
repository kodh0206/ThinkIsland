using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20Deadzone : MonoBehaviour
{
    public Camera myCamera;
    private Vector3 initialPosition;
    private Vector3 BlockinitialPosition;
    private bool playerEntered = false;
    private Rigidbody2D playerRigidbody;

    public GameObject playerPrefab; 
    public GameObject blockPrefab; 

    void Start()
    {
        initialPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        BlockinitialPosition = new Vector3(1f, -1f, 0);
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerEntered = true;
            ShakeCamera();
            StartCoroutine(ResetPlayerPosition());

            Mg20manager.instance.achievementFail = true;
        }
    }

    IEnumerator ResetPlayerPosition()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Mg20manager.instance.GameLevelDown();

        
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

        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground"); 
        foreach (var groundObject in groundObjects)
        {
            Destroy(groundObject);
        }

        GameObject[] BreakgroundObjects = GameObject.FindGameObjectsWithTag("BreakGround"); 
        foreach (var BreakgroundObject in BreakgroundObjects)
        {
            Destroy(BreakgroundObject);
        }
        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); 
        foreach (var jellyObject in jellyObjects)
        {
            Destroy(jellyObject);
        }
        yield return new WaitForSeconds(2f);

        if (playerEntered)
        {
            
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.transform.position = initialPosition;

            GameObject newStartBlock = Instantiate(blockPrefab);
            newStartBlock.transform.position = BlockinitialPosition;


            
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
        Vibration.Instance.Vibrate();
        myCamera.transform.DOShakePosition(1.0f, 0.6f, 10);  
    }

}
