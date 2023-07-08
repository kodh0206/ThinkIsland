using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2Jelly : MonoBehaviour
{

    private float height = 2f;
    private float duration = 1.5f;

    private Vector2 startPoint;
    private Vector2 endPoint;
    private float elapsedTime = 0f;
    private bool isMoving = false;

    public int score = 0;

    private void Start()
    {
        startPoint = new Vector2(0f, -6f);
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
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        score += 1;
                        //MiniGameManager.Instance.AddJelly();
                        gameObject.SetActive(false);

                        break;
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
