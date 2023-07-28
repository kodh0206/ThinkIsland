using UnityEngine;
using System;


public class MobileTouch : MonoBehaviour
{
   private Vector3 minPosition = new Vector3(-32, -20, -20);
    private Vector3 maxPosition = new Vector3(28, 30, 20);
    private Vector3 springVelocity = Vector3.zero;
    public float moveSpeed = 5f;
    public float scrollSpeed = 0.1f; // Scroll speed for zooming
    public float minOrthographicSize = 1f; // Min zoom
    public float maxOrthographicSize = 20f; // Max zoom
    public float inertiaDuration = 1.0f;
    public float springStrength = 10.0f;
    public float springDamping = 1.0f;
    private Vector3 moveVelocity = Vector3.zero;
    private bool isMoving = false;
    private Vector2 prePos, curPos, movePosDiff;
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
            pos += Vector3.Lerp(Vector3.zero, moveVelocity, inertiaDuration * Time.deltaTime);
            moveVelocity *= (1.0f - Time.deltaTime);

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
            movePosDiff = (Vector2)(prePos - curPos) * Time.deltaTime;
            moveVelocity = new Vector3(movePosDiff.x, movePosDiff.y, 0) * moveSpeed;
            prePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }
        // Scroll handling code is omitted for brevity
    }

    void TouchMove_Zoom()
    {
        // Zoom handling code is omitted for brevity

       if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Camera.main.orthographicSize += deltaMagnitudeDiff * scrollSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minOrthographicSize, maxOrthographicSize);
        }

     
    if (Input.touchCount == 1)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            prePos = touch.position - touch.deltaPosition;
            isMoving = true;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Vector3 deltaPosition = new Vector3(touch.deltaPosition.x, touch.deltaPosition.y, 0) * moveSpeed * Time.deltaTime;
            Camera.main.transform.position -= deltaPosition;

            curPos = touch.position - touch.deltaPosition;
            movePosDiff = (Vector2)(prePos - curPos) * Time.deltaTime;
            moveVelocity = new Vector3(movePosDiff.x, movePosDiff.y, 0) * moveSpeed;
            prePos = touch.position - touch.deltaPosition;
        }
        if (touch.phase == TouchPhase.Ended)
        {
            isMoving = false;
        }
    }
}

}
