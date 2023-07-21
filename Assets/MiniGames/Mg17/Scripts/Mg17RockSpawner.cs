using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg17RockSpawner : MonoBehaviour
{
    public GameObject Rock;

    public Transform player;

    public float RockSpeed = 20.0f; // ������ �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 0.35f;

    float time = -0.24f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > time_diff)
        {
            GameObject new_Rock = Instantiate(Rock);

            // ��ǥ�� �����ϰ� �����Ͽ� ����
            Vector2 spawnPosition = new Vector2(player.position.x-0.1f , player.position.y+0.1f); //�÷��̾ �������� ����
            new_Rock.transform.position = spawnPosition;



            new_Rock.GetComponent<Mg17Rock>().SetSpeed(RockSpeed); // ����ü�� ���ǵ� ����
            time = 0;
            Destroy(new_Rock, 5.0f);
        }
    }

    public void IncreaseSpeed()
    {
        RockSpeed += 1.0f; // ������ ���ǵ� ����
        time_diff -= 0.1f; // ������ ���� ���� ����
    }

    public void DecreaseSpeed()
    {
        RockSpeed -= 1.0f; // ������ ���ǵ� ����
        time_diff += 0.1f; // ������ ���� ���� ����
    }

    public void GetHit()
    {
        StartCoroutine(DisableSpawning());
    }

    private IEnumerator DisableSpawning()
    {
        // ���� ����
        time_diff = Mathf.Infinity;

        // ��� �ð�
        yield return new WaitForSeconds(2f);

        // ���� �簳
        time_diff = 0.5f;
    }
}
