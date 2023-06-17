using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2ObjectManager : MonoBehaviour
{
    public int ballArrival = 0; // ball ���� Ƚ���� ��Ÿ���� ����

    public int jellyArrival = 0; // jelly ���� Ƚ���� ��Ÿ���� ����

    public GameObject ball;
    public GameObject jelly;

    private void Start()
    {
        ball.SetActive(true);
        jelly.SetActive(false);
    }

    private void Update()
    {
        // ���� ���� ���� ������ 2:1�� �ǵ��� ����
        if (ballArrival == 2)
        {
            ball.SetActive(false);
            jelly.SetActive(true);

            ballArrival = 0; // ball ���� Ƚ�� �ʱ�ȭ
        }
        else if (jellyArrival == 1)
        {
            ball.SetActive(true);
            jelly.SetActive(false);

            jellyArrival = 0; // jelly ���� Ƚ�� �ʱ�ȭ
        }
    }
}
