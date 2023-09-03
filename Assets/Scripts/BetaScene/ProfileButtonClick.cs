using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileButtonClick : MonoBehaviour
{

    public GameObject sngCanvas;
    public MonoBehaviour mobileTouchScript;
    public TextMeshProUGUI textMeshProUGUI;


    // 프로필 버튼이 클릭될 때마다 갱신
    public void ProfileTextChange()
    {

        // GameController.Instance.charCollectionProgress = GameController.Instance.unlockedMiniGames.Count;

        string updatedText = $"젤리 획득 {GameController.Instance.jellyCount}개\n" +
                            $"골드 획득 {GameController.Instance.goldAmount}골드\n" +
                            $"탐험 입장 {GameController.Instance.playMiniGame}회\n" +
                            $"장애물 충돌없이 클리어 {GameController.Instance.noCrashObject}회\n" +
                            $"퀴즈 정답 {GameController.Instance.quizCorrect}회\n" +
                            $"골든벨 {GameController.Instance.goldenBell}회\n" +
                            $"맵 확장 단계 {GameController.Instance.mapExtend}단계\n" +
                            $"캐릭터 수집 진행도 {GameController.Instance.charCollectionProgress}/20마리\n" +
                            $"업적 달성 진행도 {GameController.Instance.achievementProgress}/0개\n" +
                            $"게임 플레이타임 {GameController.Instance.playTime}시간";

    
        // TextMeshPro 객체에 업데이트된 문자열 할당
        textMeshProUGUI.text = updatedText;
    }

    void Update()
    {
        
    }

    public void ProfileCloseButtonClick()
    {   AudioManager.Instance.PlayPressed();
        gameObject.SetActive(false);
        sngCanvas.SetActive(true);
        mobileTouchScript.enabled = true;
    }
}
