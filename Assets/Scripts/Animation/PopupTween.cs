using UnityEngine;
using DG.Tweening;

public class PopupTween : MonoBehaviour
{
    public bool showOnStart = false;
    public float popDuration = 0.5f;
    public Vector3 endScale = Vector3.one;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        if (!showOnStart)
        {
            rectTransform.localScale = Vector3.zero;
        }
    }
    private void OnEnable()
    {
        ShowPopup();
    }
    private void OnDisable()
    {
        HidePopup();
    }

    public void ShowPopup()
    {
        rectTransform.DOScale(endScale, popDuration).SetEase(Ease.OutBack);
    }

    public void HidePopup()
    {
        rectTransform.DOScale(Vector3.zero, popDuration).SetEase(Ease.InBack);
    }
}