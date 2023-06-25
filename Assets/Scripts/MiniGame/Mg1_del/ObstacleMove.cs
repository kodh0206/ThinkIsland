using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    float objSpeed;
    public Vector2 startPosition;

    // ������Ʈ�� �ٽ� Ȱ��ȭ�� ������ �ʱ� ��ġ�� �̵�
    void OnEnable()
    {
        transform.position = startPosition;
        objSpeed = Random.Range(5f, 10f);
    }

    void Update()
    {
        // �������� �̵�
        transform.Translate(Vector2.left * Time.deltaTime * objSpeed);

        // �������� �Ѿ�� ��Ȱ��ȭ
        if (transform.position.x < - 16)
        {
            gameObject.SetActive(false);
        }
    }
}
