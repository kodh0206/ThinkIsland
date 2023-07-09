using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20Brokenblock : MonoBehaviour
{
    public GameObject breakEffect; // �μ��� �� ����� ��ƼŬ ȿ��

    public float blockspeed = 3f; // ������Ʈ�� �ӵ�

    private void Update()
    {
        // ������Ʈ�� ���� �̵���Ŵ
        transform.Translate(Vector3.up * blockspeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            BreakObject();
        }
    }

    private void BreakObject()
    {   //AudioManager.Instance.BreakPlatform();
        // ��ƼŬ ȿ�� ���
        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }

        // 0.5�� ���� �� ������Ʈ ����
        Invoke("DestroyObject", 0.5f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void SetSpeed(float speed)
    {
        blockspeed = speed;
    }
}
