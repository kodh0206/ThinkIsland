using UnityEngine;
using System;


public class MobileTouch : MonoBehaviour
{
    float touchDistanceOld = 0f;       // 터치 이전 거리를 저장합니다.
    float fieldOfView = 60f;     // 카메라의 FieldOfView의 기본값을 60으로 정합니다.
    Vector2?[] touchPrevPos = { null, null };
    Vector2 touchPrevVector;
    Vector2 prePos, curPos, movePosDiff; //prevPos는 터치와 마우스drag에서 모두 사용함. 만일 구분이 필요하면 마우스 드래그용 prevPos를 별도 변수로 뺄 것
    float touchPrevDist;
    float dragSpeed = 30.0f;

    public Vector2 minPos = new Vector2(-10, -10);
    public Vector2 maxPos = new Vector2(10, 10);
    public float scrollSpeed = 2000.0f;

    void LateUpdate()
    {
        //터치가 없으면 null로 초기화
        if (0 == Input.touchCount)
        {
            touchPrevPos[0] = null;
            touchPrevPos[1] = null;
        }
    }

    void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        TouchMove_Zoom();
#else
        MouseMove_Zoom();
#endif
    }

    void MouseMove_Zoom()
{
    if (Input.GetMouseButtonDown(0)) prePos = Input.mousePosition;
    if (Input.GetMouseButton(0))
    {
        curPos = Input.mousePosition;
        movePosDiff = (Vector2)(prePos - curPos) * Time.deltaTime;
        
        Vector3 newPosition = Camera.main.transform.position - new Vector3(movePosDiff.x, movePosDiff.y, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, 0, 15);  // x 범위 제한
        newPosition.y = Mathf.Clamp(newPosition.y, -13, 0);  // y 범위 제한
        newPosition.z = Mathf.Clamp(newPosition.z, -20, 20);  // z 범위 제한
        
        Camera.main.transform.position = newPosition;
        
        prePos = Input.mousePosition;
    }
}

    // 터치를 이용한 zoom in/out 및 화면 이동
    void TouchMove_Zoom()
    {
        float touchDistance = 0f;
        float fDis = 0f;

        //줌인 줌아웃을 위한 2손가락 터치
        if (Input.touchCount == 2 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved))
        {
            touchDistance = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;
            fDis = (touchDistance - touchDistanceOld) * 0.01f;
            fieldOfView -= fDis;  // 이전 두 터치의 거리와 지금 두 터치의 거리의 차이를 FleldOfView를 차감
            fieldOfView = Mathf.Clamp(fieldOfView, 20.0f, 200.0f); // 최대는 200, 최소는 20으로 더이상 증가 혹은 감소가 되지 않도록 합니다.
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fieldOfView, Time.deltaTime * 5);  // 확대 / 축소가 갑자기 되지않도록 보간
            touchDistanceOld = touchDistance;
        }
        //움직이기위한 기타 터치 처리
       else if (Input.touchCount == 1)
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            prePos = touch.position - touch.deltaPosition;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            curPos = touch.position - touch.deltaPosition;
            movePosDiff = (Vector2)(prePos - curPos) * Time.deltaTime;
            prePos = touch.position - touch.deltaPosition;

            Vector3 newPosition = Camera.main.transform.position - new Vector3(movePosDiff.x, 0, movePosDiff.y);
            newPosition.x = Mathf.Clamp(newPosition.x, minPos.x, maxPos.x);
            newPosition.z = Mathf.Clamp(newPosition.z, minPos.y, maxPos.y);

            Camera.main.transform.position = newPosition;
        }
    }
    }
}
