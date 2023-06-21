using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private float countdownDuration; // 타이머 초기 설정값
    private float countdownTimer; // 현재 타이머 값

    private void Update()
    {
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0)
            {
                //GameManager.Instance.LoadNextScene(); // 타이머 종료 시 다음 씬 로드
            }
        }
    }

    public void Initialize(float duration)
    {
        countdownDuration = duration;
        countdownTimer = duration;
    }

    public void StartTimer()
    {
        countdownTimer = countdownDuration;
    }
}