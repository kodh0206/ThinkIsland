
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg8Spawner : MonoBehaviour
{
    public GameObject Mg8Obstacle;
    public GameObject Mg8Obstacle2;
    public GameObject Mg8Obstacle3;
    public GameObject Mg8Obstacle4;

    [SerializeField]
    private float Mg8ObstacleSpeed = 5.0f; 

    [SerializeField]
    private float time_diff = 1.5f; 
    [SerializeField]
    private int minNumObstaclesToSpawn = 1;
    [SerializeField]
    private int maxNumObstaclesToSpawn = 1; 

    float time = 0;

    public int WhatObstacle;

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
                

                WhatObstacle = Random.Range(0, 5);

                GameObject new_Mg8Obstacle;
                if (WhatObstacle == 0)
                {
                    new_Mg8Obstacle = Instantiate(Mg8Obstacle);
                }

                else if (WhatObstacle == 1)
                {
                    new_Mg8Obstacle = Instantiate(Mg8Obstacle2);
                }
                else if (WhatObstacle == 2)
                {
                    new_Mg8Obstacle = Instantiate(Mg8Obstacle3);
                }
                else
                {
                    new_Mg8Obstacle = Instantiate(Mg8Obstacle4);
                }


               
                Vector2 spawnPosition = new Vector2(15f, -0.5f);
                new_Mg8Obstacle.transform.position = spawnPosition;

                new_Mg8Obstacle.GetComponent<Mg8Tree>().SetSpeed(Mg8ObstacleSpeed); 
                Destroy(new_Mg8Obstacle, 8.0f);
            }

            time = 0;
        }
    }

    public void IncreaseSpeed()
    {
        Mg8ObstacleSpeed += 1.0f;
         time_diff -= 0.1f; 

        ChangeAllSpeed();

    }

    public void DecreaseSpeed()
    {
        Mg8ObstacleSpeed -= 1.0f;
        time_diff += 0.1f; 

        ChangeAllSpeed();

    }

    public void ChangeAllSpeed()
    {
        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Ground"); //Find all GroundTag
        foreach (var groundObject in groundObjects)
        {
            groundObject.GetComponent<Mg8GroundMoving>().SetSpeed(Mg8ObstacleSpeed);
        }

        GameObject[] Mg8TreeObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var Mg8Treeob in Mg8TreeObjects)
        {
            if (Mg8Treeob != null)
            {
                Mg8Tree mg8TreeComponent = Mg8Treeob.GetComponent<Mg8Tree>();
                if (mg8TreeComponent != null)
                {
                    mg8TreeComponent.SetSpeed(Mg8ObstacleSpeed);
                }
            }
        }

        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //FindAllJellyTag
        foreach (var jellyObject in jellyObjects)
        {
            if (jellyObject != null)
            {
                Mg8jelly Mg8jellyComponent = jellyObject.GetComponent<Mg8jelly>();
                if (Mg8jellyComponent != null)
                {
                    jellyObject.GetComponent<Mg8jelly>().SetSpeed(Mg8ObstacleSpeed);
                }
                
            }
        }
    }
    

}

