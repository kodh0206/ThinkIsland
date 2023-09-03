using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19blockSpanwer : MonoBehaviour
{
    public GameObject Mg19block;
    public GameObject Mg19Pinkblock;
    public GameObject Mg19Whiteblock;
    public GameObject Mg19Yellowblock;

    public int whatColor;

    public GameObject jelly;

    [SerializeField]
    private float Mg19blockSpeed = 5.0f; // �������� �ʱ� ���ǵ�

    [SerializeField]
    private float time_diff = 0.8f; // ��ֹ� ���� ����
    [SerializeField]
    private int minNumObstaclesToSpawn = 2; // �ּ� ���� ��ֹ� ����
    [SerializeField]
    private int maxNumObstaclesToSpawn = 3; // �ִ� ���� ��ֹ� ����

    float time = 0;



    List<Vector2> possiblePositions = new List<Vector2>()
        {
            new Vector2(0.3f, 7.0f),
            new Vector2(3f, 7.0f),
            new Vector2(-3f, 7.0f),
            new Vector2(6f, 7.0f),
            new Vector2(-6f, 7.0f),
        };

    private List<Vector2> spawnPositions = new List<Vector2>(); // ������ ��ġ���� ����ϴ� ����Ʈ

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
                GameObject new_Mg19block;

                whatColor = Random.Range(0, 4);

                if (whatColor == 0)
                {
                    new_Mg19block = Instantiate(Mg19block);
                }
                else if(whatColor == 1) 
                {
                    new_Mg19block = Instantiate(Mg19Pinkblock);
                }

                else if(whatColor == 2)
                {
                    new_Mg19block = Instantiate(Mg19Whiteblock);
                }
                else
                {
                    new_Mg19block = Instantiate(Mg19Yellowblock);
                }

                // ��ǥ�� �����ϰ� �����Ͽ� ����
                Vector2 spawnPosition = GetRandomSpawnPosition();
                new_Mg19block.transform.position = spawnPosition;

                new_Mg19block.GetComponent<Mg19block>().SetSpeed(Mg19blockSpeed); // ��ֹ��� ���ǵ� ����
                Destroy(new_Mg19block, 7.0f);

                if (Random.value < 0.3f)
                {
                    GameObject new_jelly = Instantiate(jelly);
                    spawnPosition.y += 0.7f;
                    new_jelly.transform.position = spawnPosition;
                    new_jelly.GetComponent<Mg19jelly>().SetSpeed(Mg19blockSpeed);
                    Destroy(new_jelly, 7.0f);
                }
            }

            time = 0;
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        // ������ ��� ��ġ ����Ʈ ����
        

        // �������� ���� ��ġ��� ������ ���ο� ����Ʈ ����
        List<Vector2> availablePositions = new List<Vector2>(possiblePositions);
        foreach (Vector2 spawnPos in spawnPositions)
        {
            availablePositions.Remove(spawnPos);
        }

        // ������ ��ġ �߿��� �����ϰ� ����
        int randomIndex = Random.Range(0, availablePositions.Count);
        Vector2 spawnPosition = availablePositions[randomIndex];

        // ���õ� ��ġ�� ���
        spawnPositions.Add(spawnPosition);

        // ����Ʈ�� ��� ���Ǿ��ٸ� �ʱ�ȭ
        if (spawnPositions.Count == possiblePositions.Count)
        {
            spawnPositions.Clear();
        }

        return spawnPosition;
    }

    public void IncreaseSpeed()
    {
        Mg19blockSpeed += 1.0f; // �ٴ��� ���ǵ� ����
        time_diff -= 0.2f;

        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground"); //Find all GroundTag
        foreach (var groundObject in groundObjects)
        {
            groundObject.GetComponent<Mg19block>().SetSpeed(Mg19blockSpeed);
        }

        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //FindAllJellyTag
        foreach (var jellyObject in jellyObjects)
        {
            jellyObject.GetComponent<Mg19jelly>().SetSpeed(Mg19blockSpeed);
        }

    }

    public void DecreaseSpeed()
    {
        Mg19blockSpeed -= 1.0f; // �ٴ��� ���ǵ� ����
        time_diff += 0.2f;

        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground"); //Find all GroundTag
        foreach (var groundObject in groundObjects)
        {
            groundObject.GetComponent<Mg19block>().SetSpeed(Mg19blockSpeed);
        }

        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //FindAllJellyTag
        foreach (var jellyObject in jellyObjects)
        {
            jellyObject.GetComponent<Mg19jelly>().SetSpeed(Mg19blockSpeed);
        }
    }






}
