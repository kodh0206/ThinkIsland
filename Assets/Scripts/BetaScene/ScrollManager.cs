using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    public RectTransform content;

    public void Awake()
    {
        AdjustContentSize();
    }

    // 스크롤 위치 조정 함수
    public void AdjustContentSize()
    {
        // 내용이 추가될 때마다 content의 크기를 조정하여 스크롤 위치 조정
        float contentHeight = CalculateContentHeight();
        content.sizeDelta = new Vector2(content.sizeDelta.x, contentHeight);
    }

    private float CalculateContentHeight()
    {
        float totalHeight = 0;

        // content의 자식 요소들의 높이를 합산하여 총 내용의 높이 계산
        for (int i = 0; i < content.childCount; i++)
        {
            totalHeight += content.GetChild(i).GetComponent<RectTransform>().rect.height;
        }

        return totalHeight;
    }
}
