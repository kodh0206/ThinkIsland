using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChickenRunAndJumpAnim : MonoBehaviour
{
    public float moveDistance = 1f; // 움직일 거리
    public float moveDuration = 1f; // 움직이는 데 걸리는 시간
    public float pauseDurationA = 1.5f; // A값에 해당하는 대기 시간
    public float pauseDurationB = 1.5f; // B값에 해당하는 대기 시간

    private void Start()
    {
        // 움직이는 방향을 정의합니다. 
        Vector3 leftup = new Vector3(-moveDistance, moveDistance, 0);
        Vector3 rightup = new Vector3(moveDistance, moveDistance, 0);
        Vector3 leftdown = new Vector3(-moveDistance, -moveDistance, 0);
        Vector3 rightdown = new Vector3(moveDistance, -moveDistance, 0);


        // Rotation을 위한 Vector3 값을 정의합니다.
        Vector3 rotateY = new Vector3(0, 180, 0);

        // Sequence를 생성하고 시작합니다.
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(rightup, moveDuration).SetRelative().SetEase(Ease.Linear))
                .AppendInterval(pauseDurationA)
                .Append(transform.DOMove(rightdown, moveDuration).SetRelative().SetEase(Ease.Linear))
                .AppendInterval(pauseDurationA)
                .Append(transform.DORotate(rotateY, moveDuration, RotateMode.LocalAxisAdd))
                .AppendInterval(pauseDurationA)
                .Append(transform.DOMove(leftdown, moveDuration).SetRelative().SetEase(Ease.Linear))
                .AppendInterval(pauseDurationA)
                .Append(transform.DOMove(leftup, moveDuration).SetRelative().SetEase(Ease.Linear))
                .AppendInterval(pauseDurationA)
                .Append(transform.DORotate(rotateY, moveDuration, RotateMode.LocalAxisAdd))
                .AppendInterval(pauseDurationA)
                .SetLoops(-1); // 무한 반복
    }
}