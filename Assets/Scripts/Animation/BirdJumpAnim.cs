using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BirdJumpAnim : MonoBehaviour
{
    public float moveDistance = 1f; // 움직일 거리
    public float moveDuration = 1f; // 움직이는 데 걸리는 시간
    public float pauseDurationA = 0.5f; // A값에 해당하는 대기 시간
    public float pauseDurationB = 1.0f; // B값에 해당하는 대기 시간

    private void Start()
    {
        // 움직이는 방향을 정의합니다. 
        Vector3 left = new Vector3(-moveDistance, 0, 0);
        Vector3 up = new Vector3(0, moveDistance, 0);
        Vector3 right = new Vector3(moveDistance, 0, 0);
        Vector3 down = new Vector3(0, -moveDistance, 0);

        // Rotation을 위한 Vector3 값을 정의합니다.
        Vector3 rotateY = new Vector3(0, 180, 0);

        // Sequence를 생성하고 시작합니다.
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(pauseDurationA)
                .Append(transform.DOMove(right, moveDuration).SetRelative().SetEase(Ease.Linear))
                .AppendInterval(pauseDurationB)
                .AppendInterval(pauseDurationA)
                .Append(transform.DOMove(up, moveDuration).SetRelative().SetEase(Ease.Linear))
                .Join(transform.DORotate(rotateY, moveDuration, RotateMode.LocalAxisAdd))
                .AppendInterval(pauseDurationB)
                .AppendInterval(pauseDurationA)
                .Append(transform.DOMove(left, moveDuration).SetRelative().SetEase(Ease.Linear))
                .AppendInterval(pauseDurationB)
                .AppendInterval(pauseDurationA)
                .Append(transform.DOMove(down, moveDuration).SetRelative().SetEase(Ease.Linear))
                .Join(transform.DORotate(rotateY, moveDuration, RotateMode.LocalAxisAdd))
                .AppendInterval(pauseDurationB)
                .SetLoops(-1); // 무한 반복
    }
}