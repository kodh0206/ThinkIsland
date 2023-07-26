using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg15Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Mg15Obstacle;

    public GameObject jelly;


    [SerializeField]
    private float Mg15ObstacleSpeed = 1.0f;

    [SerializeField]
    private float Mg15jellySpeed = 1.0f; 

    [SerializeField]
    private float time_diff = 2.5f; 
    [SerializeField]
    private int minNumObstaclesToSpawn = 3; 
    [SerializeField]
    private int maxNumObstaclesToSpawn = 5; 

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
            int numObstaclesToSpawn = Random.Range(minNumObstaclesToSpawn, maxNumObstaclesToSpawn + 1);

            for (int i = 0; i < numObstaclesToSpawn; i++)
            {



                if (Random.value < 0.7f)
                {
                    GameObject new_Mg15Obstacle = Instantiate(Mg15Obstacle);
                    
                    Vector2 spawnPosition = GetRandomSpawnPosition();
                    new_Mg15Obstacle.transform.position = spawnPosition;
                    new_Mg15Obstacle.GetComponent<Mg15Obstacle>().SetSpeed(Mg15ObstacleSpeed); 
                    AudioManager.Instance.ObstacleFly();
                    Destroy(new_Mg15Obstacle, 3.0f);
                }
                else
                {
                    GameObject new_jelly = Instantiate(jelly);
                    
                    Vector2 spawnPosition = GetRandomSpawnPosition();
                    new_jelly.transform.position = spawnPosition;
                    new_jelly.GetComponent<Mg15jelly>().SetSpeed(Mg15jellySpeed);
                    AudioManager.Instance.ObstacleFly();

                    Destroy(new_jelly, 3.0f);

                }
            }

            time = 0;
        }
    }


    private Vector2 GetRandomSpawnPosition()
    {
        
        int randomIndex = Random.Range(0, 6);

        
        Vector2[] spawnPositions = new Vector2[]
        {
        new Vector2(1.5f, 0.6f),
        new Vector2(3.5f, 0.6f),
        new Vector2(5.5f, 0.6f),
        new Vector2(-1.5f, 0.6f),
        new Vector2(-3.5f, 0.6f),
        new Vector2(-5.5f, 0.6f)
        };

        
        return spawnPositions[randomIndex];
    }

    public void IncreaseSpeed()
    {
        minNumObstaclesToSpawn += 1;
        maxNumObstaclesToSpawn += 1;
        time_diff -= 0.2f; 
    }
    public void DecreaseSpeed()
    {
        minNumObstaclesToSpawn -= 1;
        maxNumObstaclesToSpawn -= 1;
        time_diff += 0.2f;
    }

}
