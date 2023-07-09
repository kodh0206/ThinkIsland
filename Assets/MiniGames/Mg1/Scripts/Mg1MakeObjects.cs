using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1MakeObjects : MonoBehaviour
{
    public GameObject cowPrefab;
    public GameObject poopPrefab;
    public GameObject jellyPrefab;

    [SerializeField]
    private float cowInterval = 0f;
    [SerializeField]
    private float poopInitialDelay = 1.5f;
    [SerializeField]
    private float poopInterval = 3.2f;
    [SerializeField]
    private float jellyInitialDelay = 3f;
    [SerializeField]
    private float jellyInterval = 5f;

    private float cowTimer = 0f;
    private float poopTimer = 0f;
    private float jellyTimer = 0f;

    private void Update()
    {
        cowTimer += Time.deltaTime;
        poopTimer += Time.deltaTime;
        jellyTimer += Time.deltaTime;

        if (cowTimer >= cowInterval)
        {
            SpawnCow();
            cowTimer = 0f;
        }

        if (poopTimer >= poopInitialDelay)
        {
            SpawnPoop();
            poopTimer = 0f;
        }

        if (jellyTimer >= jellyInitialDelay)
        {
            SpawnJelly();
            jellyTimer = 0f;
        }
    }

    private void SpawnCow()
    {
        GameObject newObject = Instantiate(cowPrefab);
        newObject.transform.position = new Vector3(15f, Random.Range(-0.5590893f, 1.5f), 0);
        //newObject.GetComponent<Mg1Object>().SetSpeed(objectSpeed);
        Destroy(newObject, 10.0f);
    }

    private void SpawnPoop()
    {
        GameObject newObject = Instantiate(poopPrefab);
        newObject.transform.position = new Vector3(15f, Random.Range(-0.5590893f, 1.5f), 0);
        //newObject.GetComponent<Mg1Object>().SetSpeed(objectSpeed);
        Destroy(newObject, 10.0f);
    }

    private void SpawnJelly()
    {
        GameObject newObject = Instantiate(jellyPrefab);
        newObject.transform.position = new Vector3(15f, Random.Range(-0.5590893f, 1.5f), 0);
        //newObject.GetComponent<Mg1Object>().SetSpeed(objectSpeed);
        Destroy(newObject, 10.0f);
    }

    /*public void IncreaseSpeed()
    {
        objectSpeed += 2.0f;
    }*/
}
