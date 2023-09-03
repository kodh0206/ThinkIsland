using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2ObjectManager : MonoBehaviour
{
    float timer;
    float waitingTime;

    private Mg2Ball ballMove;
    private Mg2Jelly jellyMove;

    public int ballArrival = 0;
    public int jellyArrival = 0;

    public GameObject ball;
    public GameObject jelly;

    private void Start()
    {
        ballMove = ball.GetComponent<Mg2Ball>();
        jellyMove = jelly.GetComponent<Mg2Jelly>();

        ball.SetActive(true);
        jelly.SetActive(false);

        timer = 1.0f;
        waitingTime = 2.5f;
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitingTime)
        {
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

                ballArrival = 0;
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

                jellyArrival = 0;
            }
            else if (!ball.activeSelf && !jelly.activeSelf)
            {
                timer = 0;
                ball.SetActive(true);
                ballMove.StartMovement();

                ballArrival += 1;
                jellyArrival = 0;
            }
        }
    }


    public void IncreaseSpeed()
    {
        waitingTime -= 0.2f;
    }

    public void DecreaseSpeed()
    {
        waitingTime += 0.2f;
    }

}
