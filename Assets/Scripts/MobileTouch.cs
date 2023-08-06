using UnityEngine;
using System;


public class MobileTouch : MonoBehaviour
{   private Vector3 targetPosition;
   private Vector3 minPosition = new Vector3(-32, -20, -20);
    private Vector3 maxPosition = new Vector3(28, 30, 20);
    private Vector3 springVelocity = Vector3.zero;
    public float moveSpeed = 0.1f;
    public float scrollSpeed = 0.01f; // Scroll speed for zooming
    public float minOrthographicSize = 1f; // Min zoom
    public float maxOrthographicSize = 8f; // Max zoom
    public float inertiaDuration = 1.0f;
    public float springStrength = 10.0f;
    public float springDamping = 1.0f;
    float scalingFactor = 0.5f;
    private Vector3 moveVelocity = Vector3.zero;
    private bool isMoving = false;
    private Vector2 prePos, curPos, movePosDiff;
    private float targetOrthoSize;
    float smoothFactor = 0.1f;
    public float maxDelta = 5f;
    void Start()
    {
        targetPosition = Camera.main.transform.position; // 초기값 설정
    }
void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        TouchMove_Zoom();
#else
        MouseMove_Zoom();
#endif
    }
    void LateUpdate()
    {
        if (!isMoving && moveVelocity != Vector3.zero)
        {
            Vector3 pos = Camera.main.transform.position;
            pos += moveVelocity * inertiaDuration * Time.deltaTime;
        
        // 이 부분에서 moveVelocity 값을 조절하여 카메라의 움직임 속도를 줄입니다.
            moveVelocity *= (1.0f - Time.deltaTime * 5);  // 이 값을 조절하여 Inertia 속도를 변경할 수 있습니다.


            // Clamp the position
            pos = new Vector3(
                Mathf.Clamp(pos.x, minPosition.x, maxPosition.x),
                Mathf.Clamp(pos.y, minPosition.y, maxPosition.y),
                Mathf.Clamp(pos.z, minPosition.z, maxPosition.z)
            );

            // Spring effect
            Vector3 exceeded = Vector3.zero;
            if (pos.x < minPosition.x) exceeded.x = minPosition.x - pos.x;
            if (pos.y < minPosition.y) exceeded.y = minPosition.y - pos.y;
            if (pos.x > maxPosition.x) exceeded.x = maxPosition.x - pos.x;
            if (pos.y > maxPosition.y) exceeded.y = maxPosition.y - pos.y;
            springVelocity += exceeded * springStrength * Time.deltaTime;
            springVelocity -= springVelocity * springDamping * Time.deltaTime;  // 스프링 댐핑 효과
            pos += springVelocity;

            // Set the position
            Camera.main.transform.position = pos;
        }
    }



    void MouseMove_Zoom()
    {
         if (Input.GetMouseButtonDown(0))
    {
        prePos = Input.mousePosition;
        isMoving = true;
    }
    if (Input.GetMouseButton(0))
    {
        curPos = Input.mousePosition;
        Vector3 deltaPosition = new Vector3(curPos.x - prePos.x, curPos.y - prePos.y, 0) * moveSpeed * Time.deltaTime;
        Camera.main.transform.position -= deltaPosition;
        
        prePos = Input.mousePosition;
        moveVelocity = deltaPosition / Time.deltaTime; // 이동 속도를 적절하게 설정합니다.
    }
    if (Input.GetMouseButtonUp(0))
    {
        isMoving = false;
    }
        // Scroll handling code is omitted for brevity
    }

void TouchMove_Zoom()
    {
    if (Input.touchCount == 2)
    {
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

        targetOrthoSize = Camera.main.orthographicSize + deltaMagnitudeDiff * scrollSpeed;
        targetOrthoSize = Mathf.Clamp(targetOrthoSize, minOrthographicSize, maxOrthographicSize);
    }
    else if (Input.touchCount == 1) // else if를 사용하여 한 손가락 터치와 두 손가락 터치를 동시에 인식하지 않도록 합니다.
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            prePos = touch.position;
            isMoving = true;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
        Vector3 deltaPosition = new Vector3(touch.deltaPosition.x, touch.deltaPosition.y, 0) * moveSpeed * Time.deltaTime;
    
        // 입력 필터링 적용
        deltaPosition = Vector3.ClampMagnitude(deltaPosition, maxDelta);
        // targetPosition 업데이트
        targetPosition = Camera.main.transform.position - deltaPosition; 

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, smoothFactor);

        prePos = touch.position;
        moveVelocity = deltaPosition / Time.deltaTime;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            isMoving = false;
        }
    }

    // 부드러운 줌 효과를 위한 코드
    Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetOrthoSize, Time.deltaTime * 5f);
}

}
