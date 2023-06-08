using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowObstacle : MonoBehaviour
{
    public float cowSpeed = 1;
    public Vector2 startPosition = new Vector2(15, 0);

    // 소 오브젝트가 다시 활성화될 때마다 초기 위치로 이동
    void OnEnable()
    {
        transform.position = startPosition;
    }

    
    void Update()
    {
        // 왼쪽으로 이동
        transform.Translate(Vector2.left * Time.deltaTime * cowSpeed);

        // 왼쪽으로 넘어가면 비활성화
        if (transform.position.x < -5.5f)
        {
            gameObject.SetActive(false);
        }
    }
}
