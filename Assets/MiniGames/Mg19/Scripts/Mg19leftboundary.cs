using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19leftboundary : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 기존 플레이어의 위치를 반전시킴
            StartCoroutine(MakeOtherSide(other.gameObject));
        }
    }

    IEnumerator MakeOtherSide(GameObject player)
    {
        Vector3 newPosition = player.transform.position;
        newPosition.x = -newPosition.x - 0.5f;

        // 기존 플레이어의 위치를 변경
        player.transform.position = newPosition;

        // 새 위치로 이동한 플레이어를 찾아 특정 동작을 수행
        Mg19ButtonController.instance.FindPlayer();

        yield return null;
    }

}
