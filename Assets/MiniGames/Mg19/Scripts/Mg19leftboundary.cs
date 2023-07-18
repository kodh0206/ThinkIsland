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
            // ���� �÷��̾��� ��ġ�� ������Ŵ
            Vector3 newPosition = other.transform.position;
            newPosition.x = -newPosition.x - 0.5f;

            // ���ο� �÷��̾� ����
            GameObject newPlayer = Instantiate(playerPrefab, newPosition, Quaternion.identity);
        }
    }
}
