using UnityEngine;
using DG.Tweening;

public class PopupUpdownTween : MonoBehaviour
{
    public RectTransform popupRectTransform;
    public float popDuration = 1f;  // 애니메이션 진행 시간
    public Vector2 endPosition = new Vector2(0, 0);  // 팝업창이 최종적으로 도달해야 할 위치
    public AnimationCurve customBounce;

    private void Start()
    {
        // 시작 시 팝업창을 화면 밖으로 설정
        popupRectTransform.anchoredPosition = new Vector2(0, Screen.height);
    }

    private void OnEnable()
    {
        ShowPopup();
    }
//    private void OnDisable()
////    {
 //       HidePopup();
  //  }

    public void ShowPopup()
    {
        popupRectTransform.DOAnchorPos(endPosition, popDuration).SetEase(customBounce);
    }
    public void HidePopup()
    {
        popupRectTransform.DOScale(Vector3.zero, popDuration).SetEase(Ease.InBack);
    }



}