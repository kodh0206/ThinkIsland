using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg1Weather : MonoBehaviour
{
    public RectTransform target;

    public float moveDistance = 20f;
    public float moveDuration = 1f;
    public float easingStrength = 1f; // 이징 강도 조절 변수

    Vector3 startPoint;
    Vector3 endPoint;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned for Mg15Weather script.");
            return;
        }
        startPoint = transform.position;
        endPoint = target.position + new Vector3(moveDistance, 0f, 0f);

        StartWeatherAnimation();
    }

    private void StartWeatherAnimation()
    {
        DOTween.Kill(transform);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(endPoint, moveDuration).SetEase(Ease.InOutSine));
        sequence.AppendCallback(ResetObject);
        sequence.Play();
    }

    void ResetObject()
    {
        // 이동이 완료되면, 초기 위치로 돌아갑니다.
        transform.position = startPoint;
        StartWeatherAnimation();
    }
}