using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg14Spawner : MonoBehaviour
{
    public GameObject Mg14Obstacle;


    [SerializeField]
    private float Mg13ObstacleSpeed = 5.0f; 

    [SerializeField]
    private float time_diff = 1.5f; 
    [SerializeField]
    private int minNumObstaclesToSpawn = 1; 
    [SerializeField]
    private int maxNumObstaclesToSpawn = 1; 

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
                GameObject new_Mg14Obstacle = Instantiate(Mg14Obstacle);

               
                Vector2 spawnPosition = new Vector2(Random.Range(-2.7f, 3.25f), 5.5f);
                new_Mg14Obstacle.transform.position = spawnPosition;

                Destroy(new_Mg14Obstacle, 5.0f);
            }

            time = 0;
        }
    }


 

    public void IncreaseSpeed()
    {
        
        time_diff -= 0.1f; 
    }
    public void DecreaseSpeed()
    {

        time_diff += 0.1f;
    }

}
