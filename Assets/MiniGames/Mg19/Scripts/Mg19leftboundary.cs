using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19leftboundary : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ���� �÷��̾��� ��ġ�� ������Ŵ
            StartCoroutine(MakeOtherSide(other.gameObject));
        }
    }

    IEnumerator MakeOtherSide(GameObject player)
    {
        Vector3 newPosition = player.transform.position;
        newPosition.x = -newPosition.x - 0.5f;

        // ���� �÷��̾��� ��ġ�� ����
        player.transform.position = newPosition;

        // �� ��ġ�� �̵��� �÷��̾ ã�� Ư�� ������ ����
        Mg19ButtonController.instance.FindPlayer();

        yield return null;
    }

}
