using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20BlockSpawner : MonoBehaviour
{
    public GameObject NomalBlock;
    public GameObject BrokenBlock;

    public GameObject jelly;

    [SerializeField]
    private float BlockSpeed = 2.0f; // �������� �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 1.5f;

    [SerializeField]
    private int minNumObstaclesToSpawn = 1; // �ּ� ���� ��ֹ� ��Ʈ ����
    [SerializeField]
    private int maxNumObstaclesToSpawn = 1; // �ִ� ���� ��ֹ� ��Ʈ ����

    float time = 0;

    int Blockset;

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
            int numObstaclesToSpawn = Random.Range(minNumObstaclesToSpawn, maxNumObstaclesToSpawn + 1);

            for (int i = 0; i < numObstaclesToSpawn; i++)
            {

                Vector2 spawnPosition = GetRandomSpawnPosition();

                Blockset = Random.Range(0, 4);

                // �����ϰ� ������ �����Ͽ� ����
                if (Blockset == 0)
                {
                    GameObject new_LeftBlock = Instantiate(NomalBlock);
                    GameObject new_RightBlock = Instantiate(NomalBlock);

                    spawnPosition.x += 0.73f;
                    new_LeftBlock.transform.position = spawnPosition;

                    spawnPosition.x -= 1.12f;
                    new_RightBlock.transform.position = spawnPosition;



                    new_LeftBlock.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed); // ��ֹ��� ���ǵ� ����
                    new_RightBlock.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed); // ��ֹ��� ���ǵ� ����

                    Destroy(new_LeftBlock, 5.0f);
                    Destroy(new_RightBlock, 5.0f);


                }
                else if (Blockset == 1)
                {
                    GameObject new_LeftBlock = Instantiate(NomalBlock);
                    GameObject new_RightBlock = Instantiate(BrokenBlock);

                    spawnPosition.x += 0.73f;
                    new_LeftBlock.transform.position = spawnPosition;

                    spawnPosition.x -= 1.12f;
                    new_RightBlock.transform.position = spawnPosition;




                    new_LeftBlock.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed); // ��ֹ��� ���ǵ� ����
                    new_RightBlock.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed); // ��ֹ��� ���ǵ� ����

                    Destroy(new_LeftBlock, 5.0f);
                    Destroy(new_RightBlock, 5.0f);
                }
                else if(Blockset == 2)
                {
                    GameObject new_LeftBlock = Instantiate(BrokenBlock);
                    GameObject new_RightBlock = Instantiate(NomalBlock);

                    spawnPosition.x += 0.73f;
                    new_LeftBlock.transform.position = spawnPosition;

                    spawnPosition.x -= 1.12f;
                    new_RightBlock.transform.position = spawnPosition;


                    new_LeftBlock.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed); 
                    new_RightBlock.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed); 

                    Destroy(new_LeftBlock, 5.0f);
                    Destroy(new_RightBlock, 5.0f);
                }
                else if (Blockset == 3)
                {
                    GameObject new_LeftBlock = Instantiate(BrokenBlock);
                    GameObject new_RightBlock = Instantiate(BrokenBlock);

                    spawnPosition.x +=0.73f;
                    new_LeftBlock.transform.position = spawnPosition;

                    spawnPosition.x -= 1.12f;
                    new_RightBlock.transform.position = spawnPosition;

                    new_LeftBlock.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed); 
                    new_RightBlock.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed); 

                    Destroy(new_LeftBlock, 5.0f);
                    Destroy(new_RightBlock, 5.0f);
                }


                if (Random.value < 0.4f) //���� Ȯ��
                {
                    GameObject new_jelly = Instantiate(jelly);
                    spawnPosition.y += 0.7f;
                    spawnPosition.x += 0.5f;
                    new_jelly.transform.position = spawnPosition;
                    new_jelly.GetComponent<Mg20jelly>().SetSpeed(BlockSpeed);
                    Destroy(new_jelly, 7.0f);
                }

                

            }

            time = 0;
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        
        int randomIndex = Random.Range(0, 3);

        
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(0f, -5.5f),
        new Vector2(2.45f, -5.5f),
        new Vector2(-2.2f, -5.5f),
        };

        
        return spawnPositions[randomIndex];
    }


    public void IncreaseSpeed()
    {
        BlockSpeed += 1.0f; 
        time_diff -= 0.1f;

        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground"); //필드 파괴
        foreach (var groundObject in groundObjects)
        {
            groundObject.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed);
        }

        GameObject[] BreakgroundObjects = GameObject.FindGameObjectsWithTag("BreakGround"); //필드 파괴
        foreach (var BreakgroundObject in BreakgroundObjects)
        {
            BreakgroundObject.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed);
        }
        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //필드 젤리 파괴
        foreach (var jellyObject in jellyObjects)
        {
            jellyObject.GetComponent<Mg20jelly>().SetSpeed(BlockSpeed);
        }

    }

    public void DecreaseSpeed()
    {
        BlockSpeed -= 1.0f; 
        time_diff += 0.1f;

        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground"); //필드 파괴
        foreach (var groundObject in groundObjects)
        {
            groundObject.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed);
        }

        GameObject[] BreakgroundObjects = GameObject.FindGameObjectsWithTag("BreakGround"); //필드 파괴
        foreach (var BreakgroundObject in BreakgroundObjects)
        {
            BreakgroundObject.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed);
        }
        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //필드 젤리 파괴
        foreach (var jellyObject in jellyObjects)
        {
            jellyObject.GetComponent<Mg20jelly>().SetSpeed(BlockSpeed);
        }

    }
}
