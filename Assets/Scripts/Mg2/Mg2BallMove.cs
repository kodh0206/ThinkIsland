using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2BallMove : MonoBehaviour
{
    private Mg2ObjectManager objectManager; // Mg2ObjectManager 스크립트 참조

    private float height = 2f; // 포물선의 높이
    private float duration = 0.8f; // 포물선 이동에 걸리는 시간

    private Vector2 startPoint;
    private Vector2 endPoint;
    private float elapsedTime = 0f;
    private bool isMoving = false;

    public int colliderCheck = 0; // 충돌 체크 변수 (난이도 조절)

    private void Start()
    {
        objectManager = FindObjectOfType<Mg2ObjectManager>(); // Mg2ObjectManager 스크립트를 찾아 할당

        startPoint = new Vector2(0f, -4f);
        RandomizeEndPoint(); // endPoint를 랜덤하게 설정
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

                // endPoint에 도달했을 때 'Player' 오브젝트와 충돌 체크
                Collider2D[] colliders = Physics2D.OverlapCircleAll(endPoint, 0.5f);
                foreach (Collider2D collider in colliders)
                {
                    if (!collider.gameObject.CompareTag("Player"))
                    {
                        // 'Player' 오브젝트와 충돌한 경우
                        colliderCheck += 1; // colliderCheck 변수에 1 추가 (난이도 조절)
                        Debug.Log("못 막았다...!" + colliderCheck);
                    }
                }
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
