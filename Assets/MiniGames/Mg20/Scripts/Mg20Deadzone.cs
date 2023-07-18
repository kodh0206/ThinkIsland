using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20Deadzone : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 BlockinitialPosition;
    private bool playerEntered = false;
    private Rigidbody2D playerRigidbody;

    public GameObject playerPrefab; // Player �������� �Ҵ��ϼ���
    public GameObject blockPrefab; //����ۿ� ���

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
            StartCoroutine(ResetPlayerPosition());
        }
    }

    IEnumerator ResetPlayerPosition()
    {
        // Player ������Ʈ�� ã���ϴ�.
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Mg20manager.instance.GameLevelDown();

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

        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground"); //�ʵ� �ı�
        foreach (var groundObject in groundObjects)
        {
            Destroy(groundObject);
        }

        GameObject[] BreakgroundObjects = GameObject.FindGameObjectsWithTag("BreakGround"); //�ʵ� �ı�
        foreach (var BreakgroundObject in BreakgroundObjects)
        {
            Destroy(BreakgroundObject);
        }
        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //�ʵ� ���� �ı�
        foreach (var jellyObject in jellyObjects)
        {
            Destroy(jellyObject);
        }
        yield return new WaitForSeconds(2f);

        if (playerEntered)
        {
            // Player�� �������� ����Ͽ� �����մϴ�.
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.transform.position = initialPosition;

            GameObject newStartBlock = Instantiate(blockPrefab);
            newStartBlock.transform.position = BlockinitialPosition;


            // Player ������Ʈ�� �ٽ� Ȱ��ȭ�Ͽ� ���� ���� ���·� ����ϴ�.
            Player newPlayerComponent = newPlayer.GetComponent<Player>();
            if (newPlayerComponent != null)
            {
                newPlayerComponent.enabled = true;
            }

            playerEntered = false;
        }
    }
}
