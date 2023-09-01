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
    private EdgeCollider2D edgeCollider;

    void Start()
    {
        initialPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        edgeCollider = GetComponent<EdgeCollider2D>();
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
        // Player 오브젝트를 찾습니다.
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Player 컴포넌트를 비활성화하여 조작 불가능 상태로 만듭니다.
        Player playerComponent = player.GetComponent<Player>();


        Mg9manager.instance.GameLevelDown();

        if (playerComponent != null)
        {
            playerComponent.enabled = false;
        }

       
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero;
        }
        edgeCollider.enabled = false;

        GameObject[] ObstacleObjects = GameObject.FindGameObjectsWithTag("Obstacle"); 
        foreach (var ObstacleObject in ObstacleObjects)
        {
            Destroy(ObstacleObject);
        }
        // 오브젝트를 부수고 2초 대기합니다.
        yield return new WaitForSeconds(2f);
        player.transform.position = initialPosition;
        edgeCollider.enabled = true;

    }



    public void ShakeCamera()
    {
        Vibration.Instance.Vibrate();
        myCamera.transform.DOShakePosition(1.0f, 0.6f, 10);  
    }
}
