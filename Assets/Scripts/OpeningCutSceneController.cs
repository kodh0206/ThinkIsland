using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class OpeningCutSceneController : MonoBehaviour
{
     
     public VideoPlayer videoPlayer; // VideoPlayer 컴포넌트에 대한 참조를 설정하세요.

    private void Start()
    {
        // BGM을 정지합니다.
        AudioManager.Instance.StopBGM();

        // 비디오 재생 완료 이벤트를 구독합니다.
        videoPlayer.loopPointReached += OnVideoEnded;
    }

    private void OnDestroy()
    {
        // 비디오 재생 완료 이벤트 구독을 취소합니다.
        videoPlayer.loopPointReached -= OnVideoEnded;
    }

    private void OnVideoEnded(VideoPlayer vp)
    {
        // 비디오 재생이 끝나면 메인 씬으로 전환합니다.
        SceneManager.LoadScene("Dialog1");
        PlayerPrefs.SetInt("hasPlayed", 1);
        PlayerPrefs.Save();
    }
}