using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg18deadzone : MonoBehaviour
{
    public Camera myCamera;
    private Vector3 initialPosition;
    private bool playerEntered = false;
    private Rigidbody2D playerRigidbody;

    public GameObject playerPrefab; // Player �������� �Ҵ��ϼ���

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
        // Player ������Ʈ�� ã���ϴ�.
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Mg18manager.instance.GameLevelDown(); //levelDown

        // Player ������Ʈ�� ��Ȱ��ȭ�Ͽ� ���� �Ұ��� ���·� ����ϴ�.
        Player playerComponent = player.GetComponent<Player>();
        if (playerComponent != null)
        {
            playerComponent.enabled = false;
        }

        // Player�� Rigidbody2D�� ���ߵ��� �����մϴ�.
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero;
        }

        // ������Ʈ�� �μ��� 2�� ����մϴ�.
        Destroy(player);
        yield return new WaitForSeconds(2f);

        if (playerEntered)
        {
            // Player�� �������� ����Ͽ� �����մϴ�.
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.transform.position = initialPosition;

            // Player ������Ʈ�� �ٽ� Ȱ��ȭ�Ͽ� ���� ���� ���·� ����ϴ�.
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
        myCamera.transform.DOShakePosition(1.0f, 0.6f, 10);  // ī�޶� 1�� ����, ���� 0.4�� 20�� ���ϴ�.
    }

}
