using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20Shadow : MonoBehaviour
{
    public GameObject shadowPrefab; // 그림자 프리팹
    public Transform player; // 플레이어 Transform
    private GameObject shadowInstance; // 현재 그림자 인스턴스
    public GameObject shadowSensor; // "Ground"를 감지하는 콜라이더가 달린 게임오브젝트

    public float minHeight, maxHeight; // 플레이어의 최소 및 최대 높이
    public float maxScale, minScale; // 그림자의 최대 및 최소 스케일


    // "ShadowSensor"가 "Ground" 태그를 가진 오브젝트에 진입했을 때
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("BreakGround"))
        {
            Debug.Log("그림자닿음");
            if (shadowInstance == null)
            {
                // 그림자 인스턴스를 생성하고, 그 위치를 플레이어의 현재 위치로 설정합니다. 그림자의 Y좌표는 Ground 오브젝트의 Y좌표에 고정됩니다.
                shadowInstance = Instantiate(shadowPrefab, new Vector3(player.position.x, other.transform.position.y, player.position.z), Quaternion.identity);
                shadowInstance.transform.SetParent(player);
                // Mg20ShadowMove 스크립트를 가진 오브젝트에 그림자 인스턴스를 전달
                Mg20ShadowMove shadowMoveScript = GetComponent<Mg20ShadowMove>();
                if (shadowMoveScript)
                {
                    shadowMoveScript.SetShadowInstance(shadowInstance);
                }
            }
        }
    }

    // "ShadowSensor"가 "Ground" 태그를 가진 오브젝트에서 벗어났을 때
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("BreakGround"))
        {
            // 그림자 인스턴스를 제거합니다.
            if (shadowInstance)
            {
                Destroy(shadowInstance);
                shadowInstance = null;
            }
        }
    }
}
