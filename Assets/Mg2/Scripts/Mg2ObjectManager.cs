using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2ObjectManager : MonoBehaviour
{
    float timer;
    float waitingTime;

    private Mg2BallMove ballMove; // Mg2BallMove ��ũ��Ʈ ����
    private Mg2JellyMove jellyMove; // Mg2JellyMove ��ũ��Ʈ ����

    public int ballArrival = 0; // ball ���� Ƚ���� ��Ÿ���� ����
    public int jellyArrival = 0; // jelly ���� Ƚ���� ��Ÿ���� ����

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
            // ���� ���� ���� ������ 2:1�� �ǵ��� ���� (�ð�)
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

                ballArrival = 0; // ball ���� Ƚ�� �ʱ�ȭ
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

                jellyArrival = 0; // jelly ���� Ƚ�� �ʱ�ȭ
            }
            // jelly�� Player�� �浹�� ���� ball�� jelly�� ��� ��Ȱ��ȭ�� ������ ���
            else if (!ball.activeSelf && !jelly.activeSelf)
            {
                timer = 0;
                ball.SetActive(true);
                ballMove.StartMovement();

                ballArrival += 1; // StartMovement() ���� �� ball ���� Ƚ�� +1
                jellyArrival = 0; // jelly ���� Ƚ�� �ʱ�ȭ
            }
        }
    }
}
