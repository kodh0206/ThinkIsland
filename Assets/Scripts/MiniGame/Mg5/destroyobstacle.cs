using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyobstacle : MonoBehaviour
{
    // Start is called before the first frame update
    
    private bool obstacleEntered = false;
    private Rigidbody2D playerRigidbody;

    public GameObject Mg5obstaclePrefab; // Player 프리팹을 할당하세요

    void Start()
    {
        
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            ResetPlayerPosition();
        }
    }

    public void  ResetPlayerPosition()
    {
        // Player 오브젝트를 찾습니다.
        GameObject obstacle = GameObject.FindGameObjectWithTag("Obstacle");

        // Player 컴포넌트를 비활성화하여 조작 불가능 상태로 만듭니다.



        // 오브젝트를 부수고 2초 대기합니다.
        Destroy(obstacle);



    }
}
