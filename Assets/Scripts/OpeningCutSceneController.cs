using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpeningCutSceneController : MonoBehaviour
{
    // Start is called before the first frame update
     public string sceneAfterOpening = "Main"; // 오프닝 이후 로드할 씬의 이름
    
    // 이 함수는 오프닝 애니메이션이나 비디오가 끝났을 때 호출되어야 합니다.
    public void EndOfOpeningCutScene()
    {
        // 오프닝 컷신을 보았음을 기록합니다.
        PlayerPrefs.SetInt("hasPlayed", 1);
        SceneManager.LoadScene(sceneAfterOpening);
    }
}
