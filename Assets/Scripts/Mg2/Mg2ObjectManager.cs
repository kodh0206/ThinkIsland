using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2ObjectManager : MonoBehaviour
{
    float timer;
    float waitingTime;

    private Mg2BallMove ballMove; // Mg2BallMove 스크립트 참조
    private Mg2JellyMove jellyMove; // Mg2JellyMove 스크립트 참조

    public int ballArrival = 0; // ball 도착 횟수를 나타내는 변수
    public int jellyArrival = 0; // jelly 도착 횟수를 나타내는 변수

    public GameObject ball;
    public GameObject jelly;

    private void Start()
    {
        ballMove = ball.GetComponent<Mg2BallMove>();
        jellyMove = jelly.GetComponent<Mg2JellyMove>();

        ball.SetActive(true);
        jelly.SetActive(false);

        timer = 0.0f;
        waitingTime = 1.2f;
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitingTime)
        {
            // 공과 젤리 등장 비율이 2:1이 되도록 설정 (시간)
            if (ball.activeSelf && ballArrival < 2)
            {
                ballMove.StartMovement();
                timer = 0;
                ballArrival += 1;
            }
            else if (ball.activeSelf && ballArrival == 2)
            {
                ball.SetActive(false);
                jelly.SetActive(true);

                ballArrival = 0; // ball 도착 횟수 초기화
            }

            else if (jelly.activeSelf && jellyArrival < 1)
            {
                jellyMove.StartMovement();
                timer = 0;
                jellyArrival += 1;
            }
            else if (jelly.activeSelf && jellyArrival == 1)
            {
                ball.SetActive(true);
                jelly.SetActive(false);

                jellyArrival = 0; // jelly 도착 횟수 초기화
            }
        }
    }
}
