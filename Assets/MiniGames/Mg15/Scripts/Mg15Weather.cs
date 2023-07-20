using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg15Weather : MonoBehaviour
{
    public RectTransform target;

    public float moveDistance = 20f;
    public float moveDuration = 1f;
    public float easingStrength = 1f; // 이징 강도 조절 변수


    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned for Mg15Weather script.");
            return;
        }

        StartWeatherAnimation();
    }

    private void StartWeatherAnimation()
    {
        Vector3 startPoint = transform.position;
        Vector3 endPoint = target.position + new Vector3(moveDistance, -moveDistance, 0f);
        transform.DOMove(endPoint, moveDuration);
    }
}