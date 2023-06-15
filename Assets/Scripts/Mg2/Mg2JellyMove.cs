using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2JellyMove : MonoBehaviour
{
    private Mg2ObjectManager objectManager; // Mg2ObjectManager ��ũ��Ʈ ����

    private float height = 2f; // �������� ����
    private float duration = 0.8f; // ������ �̵��� �ɸ��� �ð�

    private Vector2 startPoint;
    private Vector2 endPoint;
    private float elapsedTime = 0f;
    private bool isMoving = false;

    public int score = 0; // ���� ����

    private void Start()
    {
        objectManager = FindObjectOfType<Mg2ObjectManager>(); // Mg2ObjectManager ��ũ��Ʈ�� ã�� �Ҵ�

        startPoint = new Vector2(0f, -4f);
        RandomizeEndPoint(); // endPoint�� �����ϰ� ����
    }

    private void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= duration)
            {
                // �̵� �Ϸ�
                transform.position = endPoint;
                isMoving = false;

                // endPoint�� �������� �� 'Player' ������Ʈ�� �浹 üũ
                Collider2D[] colliders = Physics2D.OverlapCircleAll(endPoint, 0.5f);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        // 'Player' ������Ʈ�� �浹�� ���
                        score += 1; // score ������ 1 �߰�
                        Debug.Log("Player�� �浹! ���� ���ھ�: " + score);
                    }
                }
            }
            else
            {
                // ������ �̵�
                float t = elapsedTime / duration;
                Vector2 currentPos = ParabolicInterpolation(startPoint, endPoint, height, t);
                transform.position = currentPos;
            }
        }
    }

    // �������� �׸��� ���� �Լ�
    private Vector2 ParabolicInterpolation(Vector2 start, Vector2 end, float height, float t)
    {
        float parabolicT = Mathf.Sin(t * Mathf.PI);
        Vector2 pos = Vector2.Lerp(start, end, t);
        pos.y += parabolicT * height;
        return pos;
    }

    // endPoint�� �����ϰ� �����ϴ� �޼���
    private void RandomizeEndPoint()
    {
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                endPoint = new Vector2(4.3f, 0f);
                break;
            case 1:
                endPoint = new Vector2(0f, 0f);
                break;
            case 2:
                endPoint = new Vector2(-4.3f, 0f);
                break;
        }
    }

    // �̵� ������ ȣ���ϴ� �޼���
    public void StartMovement()
    {
        elapsedTime = 0f;
        isMoving = true;
        RandomizeEndPoint(); // endPoint�� �����ϰ� ����
    }
}
