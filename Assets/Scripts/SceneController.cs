using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Button replayButton;
    public Button playButton;
    public LogoMovement logoMovement;
    public float delayBeforeLoading = 2f; // 로딩 전 대기 시간 (애니메이션이 끝나는 시간과 맞추어 설정)
    public string sceneToLoad = "Main"; // 로드할 씬의 이름
    public CanvasGroup fadeGroup; // 화면을 어둡게 만들 CanvasGroup
    public float fadeSpeed = 1f; // 화면이 어두워지는 속도
     
    private void Start()
    {  replayButton.onClick.AddListener(Replay);
         if (PlayerPrefs.GetInt("hasPlayed") == 1)
         {
            replayButton.gameObject.SetActive(true);
         }

        // 게임 시작시에 Play 버튼을 비활성화합니다.
        playButton.interactable = false;

        // 2초 후에 EnablePlayButton 메서드를 실행합니다.
        Invoke("EnablePlayButton", 1.6f);
    }

        private void EnablePlayButton()
    {
        // Play 버튼을 활성화합니다.
        playButton.interactable = true;
    }
    public void Play()
    {
        AudioManager.Instance.StartMiniGame();
        playButton.interactable = false;
        logoMovement.StartPlay();
         if (PlayerPrefs.GetInt("hasPlayed") == 1)
        {
            // 이미 오프닝 컷신을 본 사용자는 바로 메인 씬으로 이동합니다.
            sceneToLoad = "Main"; // 로드할 씬을 메인 씬으로 설정합니다.
        }
        else
        {
            // 오프닝 컷신을 보지 않은 사용자는 오프닝 컷신을 실행합니다.
            sceneToLoad = "OpeningCutScene"; // 이 부분은 오프닝 컷신의 이름에 따라 변경하십시오.
        }
        
        StartCoroutine(LoadSceneAfterDelay());

    }

    public void Replay()
    { AudioManager.Instance.StartMiniGame();
        // 'hasPlayed' 값을 0으로 설정하여 다시 오프닝 컷신을 볼 수 있도록 함
    PlayerPrefs.SetInt("hasPlayed", 0);
    PlayerPrefs.Save(); // 변경 사항을 저장
    sceneToLoad ="OpeningCutScene";
    replayButton.interactable=false;

    // 씬을 로드하기 전에 fade out 애니메이션을 실행
    StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {

        // 화면을 점차 어둡게 한다

        yield return new WaitForSeconds(delayBeforeLoading); // 설정한 시간 동안 대기
        while (fadeGroup.alpha < 1f)
        {
            fadeGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }        
        DG.Tweening.DOTween.KillAll(); // DOTween 삭제
        yield return new WaitForSeconds(delayBeforeLoading); // 설정한 시간 동안 대기
        SceneManager.LoadSceneAsync(sceneToLoad); // 씬 로드
    }
}