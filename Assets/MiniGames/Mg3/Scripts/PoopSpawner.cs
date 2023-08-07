using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Mg3Objects; // 0, 1

    [SerializeField]
    private GameObject[] sign; // 0, 1

    [SerializeField]
    private float poopInterval = 1f;

    private Player player;

    public bool IsStun=false;


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
        while (!IsStun)
        {   
            SpawnPoop();


            yield return new WaitForSeconds(poopInterval);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (!IsStun)
        {

        }
    }

    private void SpawnPoop()
    {
        float posX = Random.Range(-4f,4f);
        Vector3 position = new Vector3(posX,6,0);
        int index;
        if (Random.value < 0.6f) // Adjust the probability here (e.g., 0.8f means 80% chance)
        {
            index = 0; // Set index to 0 more often 0==poop
        }
        else
        {
            index = 1;
        }



        Vector3 sign_position = new Vector3(posX, 4.2f, 0);  // Create Warn and jelly sign

        GameObject new_sign = Instantiate(sign[index], sign_position, Quaternion.identity);

        Destroy(new_sign, 1.0f);



        GameObject newPoop = Instantiate(Mg3Objects[index], position, Quaternion.identity);
        if (index == 1)
        {
            newPoop.tag = "jelly";
        }
        else { newPoop.tag = "poop"; }
    }

    public void DecreasePoopInterval()
    {
        
        poopInterval -= (0.2f);
        if (poopInterval < 0.2f) 
        {
            poopInterval = 0.2f;
        }
    }

    public void IncreasePoopInterval()
    {

        poopInterval += (0.2f);
       
    }


    public void GetHit()
    {
        StartCoroutine(DisableSpawning());
    }

    private IEnumerator DisableSpawning()
    {

        // 생성 멈춤
        StopSpawning();

        // 대기 시간
        yield return new WaitForSeconds(1.5f);

        // 생성 재개
        StartSpawning();
        


    }



}
