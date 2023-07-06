using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Mg1BackgroundScrolling : MonoBehaviour
{
    public float speed; // 배경이 움직이는 속도
    public Transform[] backgrounds; // 배경 담는 변수
 
    float leftPosX = -14f; // 배경의 끝 x좌표
    float rightPosX = 16f; // 배경 시작 x좌표
    float xScreenHalfSize; // 게임 화면 x좌표 절반
    float yScreenHalfSize; // 게임 화면 y좌표 절반

    void Start()
    {

        // 화면 좌표 값 초기화
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;
 
        leftPosX = -(xScreenHalfSize * 2);
        rightPosX = xScreenHalfSize * 2 * backgrounds.Length;
    }
    void Update()
    {
        // 배경 스크롤링 구현
       for(int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(-speed, 0, 0) * Time.deltaTime;
 
            if(backgrounds[i].position.x < leftPosX)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x + rightPosX, nextPos.y, nextPos.z);
                backgrounds[i].position = nextPos;
            }
        }
    }
}