using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20Shadow : MonoBehaviour
{
    public GameObject shadowPrefab; // �׸��� ������
    public Transform player; // �÷��̾� Transform
    private GameObject shadowInstance; // ���� �׸��� �ν��Ͻ�
    public GameObject shadowSensor; // "Ground"�� �����ϴ� �ݶ��̴��� �޸� ���ӿ�����Ʈ

    public float minHeight, maxHeight; // �÷��̾��� �ּ� �� �ִ� ����
    public float maxScale, minScale; // �׸����� �ִ� �� �ּ� ������


    // "ShadowSensor"�� "Ground" �±׸� ���� ������Ʈ�� �������� ��
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("BreakGround"))
        {
            Debug.Log("�׸��ڴ���");
            if (shadowInstance == null)
            {
                // �׸��� �ν��Ͻ��� �����ϰ�, �� ��ġ�� �÷��̾��� ���� ��ġ�� �����մϴ�. �׸����� Y��ǥ�� Ground ������Ʈ�� Y��ǥ�� �����˴ϴ�.
                shadowInstance = Instantiate(shadowPrefab, new Vector3(player.position.x, other.transform.position.y, player.position.z), Quaternion.identity);
                shadowInstance.transform.SetParent(player);
                // Mg20ShadowMove ��ũ��Ʈ�� ���� ������Ʈ�� �׸��� �ν��Ͻ��� ����
                Mg20ShadowMove shadowMoveScript = GetComponent<Mg20ShadowMove>();
                if (shadowMoveScript)
                {
                    shadowMoveScript.SetShadowInstance(shadowInstance);
                }
            }
        }
    }

    // "ShadowSensor"�� "Ground" �±׸� ���� ������Ʈ���� ����� ��
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("BreakGround"))
        {
            // �׸��� �ν��Ͻ��� �����մϴ�.
            if (shadowInstance)
            {
                Destroy(shadowInstance);
                shadowInstance = null;
            }
        }
    }
}
