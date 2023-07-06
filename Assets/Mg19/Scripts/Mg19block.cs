using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19block : MonoBehaviour
{
    public GameObject breakEffect; // �μ��� �� ����� ��ƼŬ ȿ��

    public float moveSpeed = 5f; // �Ʒ��� �����̴� �ӵ�

    private void Update()
    {
        // �Ʒ��� ������ ����
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   AudioManager.Instance.BreakPlatform();
            BreakObject();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void BreakObject()
    {
        // ��ƼŬ ȿ�� ���
        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }

        // ������Ʈ ����
        Destroy(gameObject);
    }



    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
