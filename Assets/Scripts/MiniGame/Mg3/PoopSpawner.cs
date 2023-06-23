using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] poops; // 0, 1

    [SerializeField]
    private float poopInterval = 1f;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        StartSpawning();
    }

    private void StartSpawning()
        {
            StartCoroutine("SpawnPoopRoutine");
        }
    

    public void StopSpawning()
        {
            StopCoroutine("SpawnPoopRoutine");
        }
    
    
    IEnumerator SpawnPoopRoutine() 
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {   
            SpawnPoop();
            yield return new WaitForSeconds(poopInterval);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPoop()
    {
        float posX = Random.Range(-3f,2.85f);
        Vector3 position = new Vector3(posX,6,0);
        int index;
        if (Random.value < 0.7f) // Adjust the probability here (e.g., 0.8f means 80% chance)
        {
            index = 0; // Set index to 0 more often 0==poop
        }
        else
        {
            index = 1;
        }



        GameObject newPoop = Instantiate(poops[index], position, Quaternion.identity);
        if (index == 1)
        {
            newPoop.tag = "poop2";
        }
        else { newPoop.tag = "poop"; }
    }

    public void DecreasePoopInterval()
    {
        player.level += 1;
        poopInterval -= (0.2f*player.level);
        if (poopInterval < 0.2f) 
        {
            poopInterval = 0.1f;
        }
    }
}
