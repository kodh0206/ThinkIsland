using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20ChimneyMove : MonoBehaviour
{
    public float Chimneyspeed = 3.0f;
    public float startPosition;
    public float endPosition;

    void Update()
    {
        // x포지션을 조금씩 이동
        transform.Translate(0, Chimneyspeed * Time.deltaTime, 0);

        // 목표 지점에 도달했다면
        if (transform.position.y <= endPosition)
        {
            //ScrollEnd();
        }
    }

    void ScrollEnd()
    {
        // 원래 위치로 초기화 시킨다.
        transform.Translate(-1 * (endPosition - startPosition), 0, 0);
    }

    public void IncreaseSpeed()
    {
        Chimneyspeed +=1.0f;
    }
    public void DecreaseSpeed()
    {
        Chimneyspeed -= 1.0f;
    }

}