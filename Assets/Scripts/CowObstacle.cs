using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowObstacle : MonoBehaviour
{
    public float cowSpeed = 1;
    public Vector2 startPosition = new Vector2(15, 0);

    // �� ������Ʈ�� �ٽ� Ȱ��ȭ�� ������ �ʱ� ��ġ�� �̵�
    void OnEnable()
    {
        transform.position = startPosition;
    }

    
    void Update()
    {
        // �������� �̵�
        transform.Translate(Vector2.left * Time.deltaTime * cowSpeed);

        // �������� �Ѿ�� ��Ȱ��ȭ
        if (transform.position.x < -5.5f)
        {
            gameObject.SetActive(false);
        }
    }
}
