using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19deadzone : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 BlockinitialPosition;
    private bool playerEntered = false;
    private Rigidbody2D playerRigidbody;

    public GameObject playerPrefab; // Player 프리팹을 할당하세요
    public GameObject blockPrefab;

    void Start()
    {
        initialPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        BlockinitialPosition = new Vector3(-0.3f,-1.5f,0);
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
        // Player 오브젝트를 찾습니다.
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Mg19manager.instance.GameLevelDown();

        // Player 컴포넌트를 비활성화하여 조작 불가능 상태로 만듭니다.
        Player playerComponent = player.GetComponent<Player>();

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

        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground"); //필드 파괴
        foreach (var groundObject in groundObjects)
        {
            Destroy(groundObject);
        }
        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //필드 젤리 파괴
        foreach (var jellyObject in jellyObjects)
        {
            Destroy(jellyObject);
        }
        yield return new WaitForSeconds(2f);

        if (playerEntered)
        {
            // Player를 프리팹을 사용하여 생성합니다.
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.transform.position = initialPosition;

            GameObject newStartBlock =Instantiate(blockPrefab);
            newStartBlock.transform.position = BlockinitialPosition;

            // Player 컴포넌트를 다시 활성화하여 조작 가능 상태로 만듭니다.
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
        Camera.main.transform.DOShakePosition(1.0f, 0.6f, 10);  // 카메라를 1초 동안, 강도 0.4로 20번 흔듭니다.
    }

}
