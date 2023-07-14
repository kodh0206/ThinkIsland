using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg15deadzone : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 initialPosition;
    private bool playerEntered = false;
    private Rigidbody2D playerRigidbody;

    public GameObject playerPrefab; // Player 프리팹을 할당하세요

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
            StartCoroutine(ResetPlayerPosition());
        }
    }

    IEnumerator ResetPlayerPosition()
    {
        // Player 오브젝트를 찾습니다.
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Player 컴포넌트를 비활성화하여 조작 불가능 상태로 만듭니다.
        Player playerComponent = player.GetComponent<Player>();


        Mg15manager.instance.GameLevelDown();

        if (playerComponent != null)
        {
            playerComponent.enabled = false;
        }

        // Player의 Rigidbody2D를 멈추도록 설정합니다.
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero;
        }

        // 오브젝트를 부수고 2초 대기합니다.
        Destroy(player);
        yield return new WaitForSeconds(2f);

        if (playerEntered)
        {
            // Player를 프리팹을 사용하여 생성합니다.
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.transform.position = initialPosition;

            // Player 컴포넌트를 다시 활성화하여 조작 가능 상태로 만듭니다.
            Player newPlayerComponent = newPlayer.GetComponent<Player>();
            if (newPlayerComponent != null)
            {
                newPlayerComponent.enabled = true;
            }

            playerEntered = false;
        }
    }
}