using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<string> miniGameScenes; // 미니게임 씬들의 이름 목록
    public int totalMiniGames = 5; // 총 미니게임 개수
    public float timeBetweenGames = 2f; // 미니게임 전환 시간 (초)

    private int currentMiniGameIndex = 0; // 현재 미니게임 인덱스
    private bool isTransitioning = false; // 미니게임 전환 중인지 여부

    private void Start()
    {
        // 첫 번째 미니게임 실행
        StartCoroutine(StartMiniGame());
    }

    private IEnumerator StartMiniGame()
    {
        yield return new WaitForSeconds(timeBetweenGames);

        // 현재 미니게임 인덱스가 총 미니게임 개수를 넘어가면 게임 종료
        if (currentMiniGameIndex >= totalMiniGames)
        {
            EndGame();
            yield break;
        }

        isTransitioning = true;

        // 이전 미니게임 씬 언로드
        if (currentMiniGameIndex > 0)
        {
            SceneManager.UnloadSceneAsync(miniGameScenes[currentMiniGameIndex - 1]);
        }

        // 현재 미니게임 씬 로드
        SceneManager.LoadScene(miniGameScenes[currentMiniGameIndex], LoadSceneMode.Additive);

        // 미니게임 시작 대기
        yield return new WaitForSeconds(timeBetweenGames);

        // 미니게임 종료
        SceneManager.UnloadSceneAsync(miniGameScenes[currentMiniGameIndex]);
        currentMiniGameIndex++;

        isTransitioning = false;

        // 다음 미니게임 실행
        StartCoroutine(StartMiniGame());
    }

    public void EndGame()
    {
        // 게임 종료 처리
        Debug.Log("Game Over");
        // 예시: 결과 표시, 점수 비교 등
    }

    public void MiniGameCompleted(int score)
    {
        // 미니게임 종료 후 처리
        Debug.Log("MiniGame Completed. Score: " + score);
        // 예시: 점수 관리, 결과 처리 등
    }
}