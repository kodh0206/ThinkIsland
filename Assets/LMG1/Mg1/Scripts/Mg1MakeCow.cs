using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1MakeCow : MonoBehaviour
{
    public GameObject cow;

    [SerializeField]
    private float cowSpeed = 5.0f;

    [SerializeField]
    private float interval = 1f;

    float time = 0;
    private int count = 1;
    private bool isCowSpawned = false;


    void Update()
    {
        time += Time.deltaTime;
        
        if (time > interval)
        {
            time = 0;
            count++;
        }

        if (count > 3)
        {
            count = 1;
        }
        
        if (count == 2 && !isCowSpawned)
        {
            GameObject new_cow = Instantiate(cow);
            new_cow.transform.position = new Vector3(15f, -0.5590893f, 0);
            new_cow.GetComponent<Mg1Cow>().SetSpeed(cowSpeed);
            Destroy(new_cow, 10.0f);
            isCowSpawned = true;
        }
        else if (count != 2)
        {
            isCowSpawned = false;
        }
    }

    public void IncreaseSpeed()
    {
        cowSpeed += 2.0f;
    }
}
