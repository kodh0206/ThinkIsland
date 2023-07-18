using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg12RockSpawner : MonoBehaviour
{
    public GameObject Rock;

    public Transform player;


    [SerializeField]
    private float RockSpeed = 5.0f; // ������ �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 1.5f;

    float time = 0;

    // 애니메이션
    public bool rockThrow = false;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > time_diff)
        {
            GameObject new_Rock = Instantiate(Rock);
            rockThrow = true;

            
            Vector2 spawnPosition = new Vector2(player.position.x , player.position.y); 
            new_Rock.transform.position = spawnPosition;

            
            
            new_Rock.GetComponent<Mg12Rock>().SetSpeed(RockSpeed); 
            time = 0;
            
            Destroy(new_Rock, 5.0f);


        }
    }

    public void IncreaseSpeed()
    {
        RockSpeed += 1.0f; 
        time_diff -= 0.1f; 
    }

    public void DecreaseSpeed()
    {
        RockSpeed -= 1.0f;
        time_diff += 0.1f;
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
