using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyobstacle : MonoBehaviour
{
    // Start is called before the first frame update
    
    private bool obstacleEntered = false;
    private Rigidbody2D playerRigidbody;

    public GameObject Mg5obstaclePrefab; // Player �������� �Ҵ��ϼ���

    void Start()
    {
        
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("obstacle"))
        {
            ResetPlayerPosition();
        }
    }

    public void  ResetPlayerPosition()
    {
        // Player ������Ʈ�� ã���ϴ�.
        GameObject obstacle = GameObject.FindGameObjectWithTag("obstacle");

        // Player ������Ʈ�� ��Ȱ��ȭ�Ͽ� ���� �Ұ��� ���·� ����ϴ�.



        // ������Ʈ�� �μ��� 2�� ����մϴ�.
        Destroy(obstacle);



    }
}
