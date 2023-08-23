using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg9jellySpawner : MonoBehaviour
{
    public GameObject jelly;

    [SerializeField]
    private float jellySpeed = 5.0f; 

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

           
            Vector2 spawnPosition = new Vector2(9.4f, Random.Range(-3.4f, 3.3f));
            new_jelly.transform.position = spawnPosition;

            

            new_jelly.GetComponent<Mg9jelly>().SetSpeed(jellySpeed); 
            time = 0;
            Destroy(new_jelly, 5.0f);
        }
    }

    public void IncreaseSpeed()
    {
        jellySpeed += 2.0f; 
        time_diff -= 0.1f; 
    }

    public void DecreaseSpeed()
    {
        jellySpeed -= 2.0f; 
        time_diff += 0.1f; 
    }

}
