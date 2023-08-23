using UnityEngine;

public class BuildingUpgradeButton : MonoBehaviour
{
    public Building targetBuilding; // 연결할 Building 오브젝트

    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // 터치 이벤트 처리
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)  // 터치 시작 부분을 감지
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;  // Z 좌표를 0으로 설정하여 2D 위치만 고려

                if (boxCollider.OverlapPoint(touchPosition))  // 터치 위치가 콜라이더 내부인지 확인
                {
                    if (targetBuilding != null)
                    {
                        targetBuilding.UpgradeBuilding();
                    }
                    else
                    {
                        Debug.LogError("Building 오브젝트가 설정되지 않았습니다.");
                    }
                }
            }
        }
    }

        private void OnMouseDown()
    {
        targetBuilding.UpgradeBuilding();
    }

}