using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg14jellySpawner : MonoBehaviour
{
    public GameObject jelly;
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

            
            Vector2 spawnPosition = new Vector2(Random.Range(-3.4f, 2.97f), 5.5f);
            new_jelly.transform.position = spawnPosition;



            time = 0;
            Destroy(new_jelly, 5.0f);
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
