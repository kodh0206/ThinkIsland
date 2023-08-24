using UnityEngine;

public class Mg20ShadowMove : MonoBehaviour
{
    private GameObject shadowInstance; // 그림자 인스턴스
    public float minHeight, maxHeight; // 플레이어의 최소 및 최대 높이
    public float maxScale, minScale; // 그림자의 최대 및 최소 스케일
    public float blockspeed = 3f;

    // 그림자 인스턴스를 설정하는 메서드
    public void SetShadowInstance(GameObject shadow)
    {
        shadowInstance = shadow;
    }

    // Update is called once per frame
    void Update()
    {
        if (shadowInstance)
        {
            // 그림자의 X좌표만 플레이어를 따라갑니다.
            shadowInstance.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            // 플레이어의 높이에 따라 그림자의 스케일을 결정
            float distance = Mathf.Clamp(transform.position.y - shadowInstance.transform.position.y, minHeight, maxHeight); // 현재 플레이어와 그림자 사이의 거리
            float t = (distance - minHeight) / (maxHeight - minHeight); // 비례 계수 계산 (0 ~ 1 범위)
            float scale = Mathf.Lerp(maxScale, minScale, t); // 플레이어와 그림자 사이의 거리에 따라 그림자의 스케일 결정

            // 그림자의 스케일을 적용
            shadowInstance.transform.localScale = new Vector3(scale, scale, 1);

            transform.Translate(Vector3.up * blockspeed * Time.deltaTime);

        }
    }
}
