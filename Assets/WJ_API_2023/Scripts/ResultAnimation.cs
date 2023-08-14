using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultAnimation : MonoBehaviour
{
    public UISpriteAnimation uiSpriteAnimation; // 애니메이션 스크립트에 대한 참조

    private void OnEnable()
    {
        // 창이 활성화될 때 애니메이션 시작
        uiSpriteAnimation.Func_PlayUIAnim();
    }

    private void OnDisable()
    {
        // 창이 비활성화될 때 애니메이션 중지
        uiSpriteAnimation.Func_StopUIAnim();
    }
}