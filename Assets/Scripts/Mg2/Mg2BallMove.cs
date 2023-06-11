using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2BallMove : MonoBehaviour
{
    private float height = 2f; // 포물선의 높이
    private float duration = 1f; // 포물선 이동에 걸리는 시간

    private Vector2 startPoint;
    private Vector2 endPoint;
    private float elapsedTime = 0f;
    private bool isMoving = false;

    private void Start()
    {
        startPoint = new Vector2(0f, -4f);
        RandomizeEndPoint(); // endPoint를 랜덤하게 설정
        InvokeRepeating("StartMovement", 0f, 1.5f); // 2초마다 StartMovement() 실행
    }

    private void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= duration)
            {
                // 이동 완료
                transform.position = endPoint;
                isMoving = false;
            }
            else
            {
                // 포물선 이동
                float t = elapsedTime / duration;
                Vector2 currentPos = ParabolicInterpolation(startPoint, endPoint, height, t);
                transform.position = currentPos;
            }
        }
    }

    // 포물선을 그리는 보간 함수
    private Vector2 ParabolicInterpolation(Vector2 start, Vector2 end, float height, float t)
    {
        float parabolicT = Mathf.Sin(t * Mathf.PI);
        Vector2 pos = Vector2.Lerp(start, end, t);
        pos.y += parabolicT * height;
        return pos;
    }

    // endPoint를 랜덤하게 설정하는 메서드
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

    // 이동 시작을 호출하는 메서드
    public void StartMovement()
    {
        elapsedTime = 0f;
        isMoving = true;
        RandomizeEndPoint(); // endPoint를 랜덤하게 변경
    }
}
