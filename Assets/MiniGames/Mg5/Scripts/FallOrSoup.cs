using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FallOrSoup : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool playerEntered = false;
    private Rigidbody2D playerRigidbody;
    public Camera myCamera;
    public GameObject playerPrefab; // Player �������� �Ҵ��ϼ���

    public GameObject StartBlcok;

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

            Mg5manager.instance.achievementFail = true;
        }
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

       
        Destroy(player);
        yield return new WaitForSeconds(2f);


        // levelDown

        Mg5manager.instance.GameLevelDown();

        if (playerEntered)
        {
            
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.transform.position = initialPosition;

            GameObject newStartBlock = Instantiate(StartBlcok);
            newStartBlock.transform.position = new Vector3(-0.08f,1.08f,0f);

          
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
        myCamera.transform.DOShakePosition(0.8f, 1f);  
    }
}
