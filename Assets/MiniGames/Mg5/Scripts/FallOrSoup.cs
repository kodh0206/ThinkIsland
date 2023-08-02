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
        }
    }

    IEnumerator ResetPlayerPosition()
    {
        // Player ������Ʈ�� ã���ϴ�.
        GameObject player = GameObject.FindGameObjectWithTag("Player");

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


        // levelDown

        Mg5manager.instance.GameLevelDown();

        if (playerEntered)
        {
            // Player�� �������� ����Ͽ� �����մϴ�.
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.transform.position = initialPosition;

            GameObject newStartBlock = Instantiate(StartBlcok);
            newStartBlock.transform.position = new Vector3(-0.08f,1.08f,0f);

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
        myCamera.transform.DOShakePosition(0.8f, 1f);  // ī�޶� 1�� ����, ���� 1�� ���ϴ�.
    }
}
