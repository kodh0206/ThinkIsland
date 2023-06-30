using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16JellyMove : MonoBehaviour
{
    public GameObject jelly;

    [SerializeField]
    private float jellySpeed = 5.0f;

    float time = 0;
    private float time_diff = 5f;

    private float minX = -9.5f;
    private float maxX = 9.5f;

    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > time_diff)
        {
            float randomX = Random.Range(minX, maxX);
            Vector2 spawnPosition = new Vector2(randomX, transform.position.y);

            GameObject newJelly = Instantiate(jelly, spawnPosition, Quaternion.identity);

            //newJelly.GetComponent<Mg16Jelly>().SetSpeed(jellySpeed);
            time = 0;
            StartCoroutine(DisableSpawning(newJelly));
            //Destroy (newJelly);
        }
    }

    private IEnumerator DisableSpawning(GameObject jellyObject)
    {
        //time_diff = Mathf.Infinity;
        //time_diff = 5.0f;
        yield return new WaitForSeconds(2f);
        Destroy(jellyObject);
        //time_diff = 5.0f;
    }
}
