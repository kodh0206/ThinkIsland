using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10jellySpawner : MonoBehaviour
{
    public GameObject jelly;

    public Transform player;

    [SerializeField]
    private float jellySpeed = 5.0f; // ������ �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 1.5f;

    float time = 0;

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
            GameObject new_jelly = Instantiate(jelly);

            // ��ǥ�� �����ϰ� �����Ͽ� ����
            Vector2 spawnPosition = new Vector2(player.position.x + Random.Range(-8.0f, 8.0f), player.position.y - 12.5f); //�÷��̾ �������� ����
            new_jelly.transform.position = spawnPosition;

            

            new_jelly.GetComponent<Mg10jelly>().SetSpeed(jellySpeed); // ������ ���ǵ� ����
            time = 0;
            Destroy(new_jelly, 5.0f);
        }
    }

    public void IncreaseSpeed()
    {
        jellySpeed += 2.0f; // ������ ���ǵ� ����
        time_diff -= 0.1f; // ������ ���� ���� ����
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
        time_diff = 1.5f;
    }



}
