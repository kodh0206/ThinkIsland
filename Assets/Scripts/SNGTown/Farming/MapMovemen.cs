using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;

    private Vector2 touchStart;
    private bool isDragging = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 direction = touch.position - touchStart;
                Vector2 moveVector = direction * moveSpeed * Time.deltaTime;

                transform.position += (Vector3)moveVector;
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, minX, maxX),
                    Mathf.Clamp(transform.position.y, minY, maxY),
                    transform.position.z
                );

                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }
}