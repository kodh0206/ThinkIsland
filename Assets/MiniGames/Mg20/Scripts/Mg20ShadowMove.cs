using UnityEngine;

public class Mg20ShadowMove : MonoBehaviour
{
    private GameObject shadowInstance; // �׸��� �ν��Ͻ�
    public float minHeight, maxHeight; // �÷��̾��� �ּ� �� �ִ� ����
    public float maxScale, minScale; // �׸����� �ִ� �� �ּ� ������
    public float blockspeed = 3f;

    // �׸��� �ν��Ͻ��� �����ϴ� �޼���
    public void SetShadowInstance(GameObject shadow)
    {
        shadowInstance = shadow;
    }

    // Update is called once per frame
    void Update()
    {
        if (shadowInstance)
        {
            // �׸����� X��ǥ�� �÷��̾ ���󰩴ϴ�.
            shadowInstance.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            // �÷��̾��� ���̿� ���� �׸����� �������� ����
            float distance = Mathf.Clamp(transform.position.y - shadowInstance.transform.position.y, minHeight, maxHeight); // ���� �÷��̾�� �׸��� ������ �Ÿ�
            float t = (distance - minHeight) / (maxHeight - minHeight); // ��� ��� ��� (0 ~ 1 ����)
            float scale = Mathf.Lerp(maxScale, minScale, t); // �÷��̾�� �׸��� ������ �Ÿ��� ���� �׸����� ������ ����

            // �׸����� �������� ����
            shadowInstance.transform.localScale = new Vector3(scale, scale, 1);

            transform.Translate(Vector3.up * blockspeed * Time.deltaTime);

        }
    }
}
