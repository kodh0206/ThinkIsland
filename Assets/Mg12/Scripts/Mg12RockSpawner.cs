using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg12RockSpawner : MonoBehaviour
{
    public GameObject Rock;

    public Transform player;
    public Mg12Player playerScript; // Mg12Player 스크립트를 참조하는 변수를 추가합니다.


    [SerializeField]
    private float RockSpeed = 5.0f; // ������ �ʱ� ���ǵ�

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
            GameObject new_Rock = Instantiate(Rock);

            // ��ǥ�� �����ϰ� �����Ͽ� ����
            Vector2 spawnPosition = new Vector2(player.position.x , player.position.y); //�÷��̾ �������� ����
            new_Rock.transform.position = spawnPosition;

            
            
            new_Rock.GetComponent<Mg12Rock>().SetSpeed(RockSpeed); // ������ ���ǵ� ����
            time = 0;
            
            Destroy(new_Rock, 5.0f);
            playerScript.PlayShootAnimation(); // Mg12player 가서 돌이 발사
            


        }
    }

    public void IncreaseSpeed()
    {
        RockSpeed += 2.0f; // ������ ���ǵ� ����
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
