using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19leftboundary : MonoBehaviour
{
    public GameObject playerPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other);
            // 기존 플레이어의 위치를 반전시킴
            Vector3 newPosition = other.transform.position;
            newPosition.x = -newPosition.x - 0.5f;

            // 새로운 플레이어 생성
            GameObject newPlayer = Instantiate(playerPrefab, newPosition, Quaternion.identity);
        }
    }
}
