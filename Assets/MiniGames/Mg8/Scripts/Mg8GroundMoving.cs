using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mg8GroundMoving : MonoBehaviour
{
    public float Groundspeed = 5f; // ���� �̵� �ӵ�
    private float groundWidth; // ���� ���� ����

    private void Start()
    {
        

        groundWidth = 4f;
        
    }

    private void Update()
    {
        // ���� �������� �̵���ŵ�ϴ�.
        transform.Translate(Vector2.left * Groundspeed * Time.deltaTime);

        // ���� �������� ����� ���������� �̵��մϴ�.
        if (transform.position.x < -groundWidth)
        {
            // ���� ���� �����ʿ� ��ġ��ŵ�ϴ�.
            float newPositionX = transform.position.x + groundWidth * 2;
            transform.position = new Vector2(newPositionX, transform.position.y);
        }
    }

    public void SetSpeed(float speed)
    {
        Groundspeed = speed;
    }
}
