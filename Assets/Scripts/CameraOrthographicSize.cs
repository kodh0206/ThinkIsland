using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrthographicSize : MonoBehaviour
{
    public Transform target; // 타일맵의 일정 부분을 카메라가 따라갈 대상
    public float padding = 1f; // 타일맵 주변에 추가적인 여백 설정

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        AdjustOrthographicSize();
    }

    void Update()
    {
        if (target.hasChanged)
        {
            AdjustOrthographicSize();
            target.hasChanged = false;
        }
    }

    void AdjustOrthographicSize()
    {
        Bounds targetBounds = CalculateTargetBounds();
        float targetOrthographicSize = CalculateTargetOrthographicSize(targetBounds);
        mainCamera.orthographicSize = targetOrthographicSize;
    }

    Bounds CalculateTargetBounds()
    {
        // 타일맵의 일정 부분의 위치와 크기 계산
        // 예: (0, 0)에서 (10, 10)까지의 타일맵을 표시하려면:
        Vector3 min = new Vector3(0, 0, 0);
        Vector3 max = new Vector3(10, 10, 0);

        Bounds targetBounds = new Bounds((min + max) * 0.5f, max - min);
        return targetBounds;
    }

    float CalculateTargetOrthographicSize(Bounds targetBounds)
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        float targetHeight = targetBounds.size.y + padding;
        float targetWidth = targetBounds.size.x + padding * aspectRatio;

        float orthographicSize = targetHeight * 0.5f;
        if (targetWidth > orthographicSize * 2f * aspectRatio)
            orthographicSize = targetWidth / (2f * aspectRatio);

        return orthographicSize;
    }
}