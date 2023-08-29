using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20BlockSpawner : MonoBehaviour
{
    public GameObject NomalBlock;
    public GameObject BrokenBlock;

    public GameObject jelly;

    [SerializeField]
    private float BlockSpeed = 2.5f; 

    [SerializeField]
    private float time_diff = 1.5f;

    [SerializeField]
    private int minNumObstaclesToSpawn = 1; 
    [SerializeField]
    private int maxNumObstaclesToSpawn = 1; 

    float time = 0;

    int Blockset;

    Vector2[] spawnPositions = new Vector2[]
{
        new Vector2(0f, -5.5f),
        new Vector2(2.45f, -5.5f),
        new Vector2(-2.2f, -5.5f),
};

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

                
                if (Blockset == 0)
                {
                    GameObject new_LeftBlock = Instantiate(NomalBlock);
                    GameObject new_RightBlock = Instantiate(NomalBlock);

                    spawnPosition.x += 0.73f;
                    new_LeftBlock.transform.position = spawnPosition;

                    spawnPosition.x -= 1.12f;
                    new_RightBlock.transform.position = spawnPosition;



                    new_LeftBlock.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed); 
                    new_RightBlock.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed); 

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




                    new_LeftBlock.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed); 
                    new_RightBlock.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed); 

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


                if (Random.value < 0.75f) 
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

        return spawnPositions[randomIndex];
    }


    public void IncreaseSpeed()
    {
        BlockSpeed += 1.0f; 
        time_diff -= 0.1f;

        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground"); //Find all GroundTag
        foreach (var groundObject in groundObjects)
        {
            groundObject.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed);
        }

        GameObject[] BreakgroundObjects = GameObject.FindGameObjectsWithTag("BreakGround"); //FindAllBackGroundTag
        foreach (var BreakgroundObject in BreakgroundObjects)
        {
            BreakgroundObject.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed);
        }
        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //FindAllJellyTag
        foreach (var jellyObject in jellyObjects)
        {
            jellyObject.GetComponent<Mg20jelly>().SetSpeed(BlockSpeed);
        }

    }

    public void DecreaseSpeed()
    {
        BlockSpeed -= 1.0f; 
        time_diff += 0.1f;

        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground"); //Find all GroundTag
        foreach (var groundObject in groundObjects)
        {
            groundObject.GetComponent<Mg20NomalBlock>().SetSpeed(BlockSpeed);
        }

        GameObject[] BreakgroundObjects = GameObject.FindGameObjectsWithTag("BreakGround"); //FindAllBackGroundTag
        foreach (var BreakgroundObject in BreakgroundObjects)
        {
            BreakgroundObject.GetComponent<Mg20Brokenblock>().SetSpeed(BlockSpeed);
        }
        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //FindAllJellyTag
        foreach (var jellyObject in jellyObjects)
        {
            jellyObject.GetComponent<Mg20jelly>().SetSpeed(BlockSpeed);
        }

    }
}
