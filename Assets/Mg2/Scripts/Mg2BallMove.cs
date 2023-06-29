using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2BallMove : MonoBehaviour
{
    private float height = 2f;
    private float duration = 0.8f;

    private Vector2 startPoint;
    private Vector2 endPoint;
    private float elapsedTime = 0f;
    private bool isMoving = false;

    private Mg2PlayerMove mg2PlayerMove;

    public int score = 0;

    private void Start()
    {
        mg2PlayerMove = GameObject.FindObjectOfType<Mg2PlayerMove>();
        startPoint = new Vector2(0f, -4f);
        RandomizeEndPoint();
    }

    private void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= duration)
            {
                transform.position = endPoint;
                isMoving = false;
                Collider2D[] colliders = Physics2D.OverlapCircleAll(endPoint, 0.5f);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Obstacle"))
                    {
                        Debug.Log("방어 성공");
                    }
                    else// if (!collider.gameObject.CompareTag("Obstacle"))
                    {
                        mg2PlayerMove.StunPlayer();
                        //collider.gameObject.GetComponent<Mg2PlayerMove>().GetObstacle();
                        Debug.Log("방어 실패");
                    }
                }
            }
            else
            {
                float t = elapsedTime / duration;
                Vector2 currentPos = ParabolicInterpolation(startPoint, endPoint, height, t);
                transform.position = currentPos;
            }
        }
    }
    private Vector2 ParabolicInterpolation(Vector2 start, Vector2 end, float height, float t)
    {
        float parabolicT = Mathf.Sin(t * Mathf.PI);
        Vector2 pos = Vector2.Lerp(start, end, t);
        pos.y += parabolicT * height;
        return pos;
    }

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

    public void StartMovement()
    {
        elapsedTime = 0f;
        isMoving = true;
        RandomizeEndPoint();
    }
}