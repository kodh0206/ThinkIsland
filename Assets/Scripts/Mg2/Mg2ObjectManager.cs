using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2ObjectManager : MonoBehaviour
{
    public int ballArrival = 0; // ball 도착 횟수를 나타내는 변수

    public int jellyArrival = 0; // jelly 도착 횟수를 나타내는 변수

    public GameObject ball;
    public GameObject jelly;

    private void Start()
    {
        ball.SetActive(true);
        jelly.SetActive(false);
    }

    private void Update()
    {
        // 공과 젤리 등장 비율이 2:1이 되도록 설정
        if (ballArrival == 2)
        {
            ball.SetActive(false);
            jelly.SetActive(true);

            ballArrival = 0; // ball 도착 횟수 초기화
        }
        else if (jellyArrival == 1)
        {
            ball.SetActive(true);
            jelly.SetActive(false);

            jellyArrival = 0; // jelly 도착 횟수 초기화
        }
    }
}
