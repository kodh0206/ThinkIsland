using UnityEngine;
using DG.Tweening;

public class Mg6BackgroundMove : MonoBehaviour
{
    public Transform playerTransform; // 플레이어의 Transform 컴포넌트
    public float moveDuration = 1.0f; // 배경 이동에 걸리는 시간
    public float punchDuration = 0.2f; // 잔상 효과의 지속 시간
    public float punchStrength = 0.2f; // 잔상 효과의 세기

    private Vector3 initialPlayerPosition; // 플레이어의 초기 위치
    private Vector3 initialBackgroundPosition; // 배경의 초기 위치

    private void Start()
    {
        initialPlayerPosition = playerTransform.position;
        initialBackgroundPosition = transform.position;
    }

    private void Update()
    {
        float playerDeltaY = playerTransform.position.y - initialPlayerPosition.y;

        // 배경 이동
        Vector3 targetPosition = initialBackgroundPosition + new Vector3(0f, -playerDeltaY * 0.2f, 0f);
        transform.DOMove(targetPosition, moveDuration);

        // 잔상 효과
        transform.DOPunchPosition(Vector3.zero, punchDuration, 1, punchStrength);
    }
}
