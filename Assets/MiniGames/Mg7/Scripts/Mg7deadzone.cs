using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg7deadzone : MonoBehaviour
{
    private Vector3 initialPosition;

    public Camera myCamera;
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

            Mg7manager.instance.GameLevelDown(); //player out and level down

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
        myCamera.transform.DOShakePosition(0.8f, 1f);  // ī�޶� 1�� ����, ���� 1�� ���ϴ�.
    }
}
