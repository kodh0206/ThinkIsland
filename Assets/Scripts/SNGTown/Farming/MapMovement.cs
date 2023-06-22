using UnityEngine;

public class Farming: MonoBehaviour
{
    private Vector3 touchStartPos; // 터치 시작 위치
    private Vector3 touchEndPos; // 터치 끝 위치

    public float moveSpeed = 10f; // 맵 이동 속도

    private void Update()
    {
        // 터치 입력 감지
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // 첫 번째 터치 정보 가져오기

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                MoveMap();
            }
        }
    }

    private void MoveMap()
    {
        float swipeDistance = (touchEndPos - touchStartPos).magnitude; // 스와이프 거리 계산

        // 스와이프 거리가 일정 값 이상인 경우에만 맵 이동
        if (swipeDistance > 50f)
        {
            Vector3 direction = touchEndPos - touchStartPos;
            direction.Normalize(); // 이동 방향 정규화

            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}