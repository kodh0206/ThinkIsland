using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2Ball : MonoBehaviour
{
    private float height = 2f;
    private float duration = 1.5f;
    private float upwardForce = 5f; // 위쪽으로 가할 힘의 크기

    private Vector2 startPoint;
    private Vector2 endPoint;
    private float elapsedTime = 0f;
    private bool isMoving = false;

    private Mg2Player mg2Player;

    public int score = 0;

    private void Start()
    {
        mg2Player = GameObject.FindObjectOfType<Mg2Player>();
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
                bool defenseSuccess = false; // 방어 성공 여부를 나타내는 변수 추가
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Obstacle"))
                    {   
                        AudioManager.Instance.GoalKeep();
                        defenseSuccess = true; // 방어 성공 표시

                        // 날아가는 공
                        // 공에 위쪽으로 힘을 가함
                        Rigidbody2D rb = GetComponent<Rigidbody2D>();
                        rb.velocity = Vector2.zero;
                        rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);

                        break;
                    }
                }
                if (!defenseSuccess)
                {
                    // 캐릭터 스턴(버튼 비활성화) + 캐릭터 색 변경
                    AudioManager.Instance.Goal();
                    mg2Player.GetObstacle();
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
    // 포물선 그리는 함수
    private Vector2 ParabolicInterpolation(Vector2 start, Vector2 end, float height, float t)
    {
        float parabolicT = Mathf.Sin(t * Mathf.PI);
        Vector2 pos = Vector2.Lerp(start, end, t);
        pos.y += parabolicT * height;
        return pos;
    }

    // endPoint 랜덤 설정 (셋 중 하나로 공 이동)
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
