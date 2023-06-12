using UnityEngine;
using UnityEngine.UI;

public class MiniGameTimer : MonoBehaviour
{
    public float gameTime = 60f; // 미니게임 시간 (초)
    public Text timerText; // 타이머를 표시할 UI 텍스트

    private float currentTime; // 현재 남은 시간

    private void Start()
    {
        currentTime = gameTime;
    }

    private void Update()
    {
        // 타이머 감소
        currentTime -= Time.deltaTime;

        // 타이머 종료 처리
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            EndGame();
        }

        // 타이머를 UI에 표시
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        // 시간을 분:초 형식으로 변환하여 UI에 표시
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = "Time: " + timeString;
    }

    private void EndGame()
    {
        // 미니게임 종료 처리
        // 예시: 점수 계산, 결과 표시, 게임 오버 등
    }
}
