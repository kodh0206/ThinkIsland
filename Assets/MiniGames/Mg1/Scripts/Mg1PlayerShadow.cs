using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1PlayerShadow : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public float fixedY; // 그림자의 고정된 y좌표
    public float minHeight, maxHeight; // 플레이어의 최소 및 최대 높이
    public float maxScale, minScale; // 그림자의 최대 및 최소 스케일

    void Update()
    {
        // 그림자의 위치를 플레이어의 x좌표와 고정된 y좌표로 설정
        transform.position = new Vector3(player.position.x, fixedY, player.position.z);
        
        // 플레이어의 높이에 따라 그림자의 스케일을 결정
        float height = Mathf.Clamp(player.position.y, minHeight, maxHeight); // 현재 플레이어의 높이
        float t = (height - minHeight) / (maxHeight - minHeight); // 비례 계수 계산 (0 ~ 1 범위)
        float scale = Mathf.Lerp(maxScale, minScale, t); // 플레이어의 높이에 따라 그림자의 스케일 결정
        
        // 그림자의 스케일을 적용
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
