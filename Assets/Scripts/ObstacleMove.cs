using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float objSpeed;
    public Vector2 startPosition;

    // ������Ʈ�� �ٽ� Ȱ��ȭ�� ������ �ʱ� ��ġ�� �̵�
    void OnEnable()
    {
        transform.position = startPosition;
    }

    
    void Update()
    {
        // �������� �̵�
        transform.Translate(Vector2.left * Time.deltaTime * objSpeed);

        // �������� �Ѿ�� ��Ȱ��ȭ
        if (transform.position.x < -5.5f)
        {
            gameObject.SetActive(false);
        }
    }
}
