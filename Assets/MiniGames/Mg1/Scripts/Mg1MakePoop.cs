using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1MakePoop : MonoBehaviour
{
    public GameObject poop;

    [SerializeField]
    private float poopSpeed = 5.0f;

    [SerializeField]
    private float interval = 1f;


    private float time = 0;
    private int count = 1;
    private bool isPoopSpawned = false;

    private void Update()
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

        if (count == 1 && !isPoopSpawned)
        {
            GameObject new_poop = Instantiate(poop);
            new_poop.transform.position = new Vector3(15f, -2, 0);
            new_poop.GetComponent<Mg1Poop>().SetSpeed(poopSpeed);
            Destroy(new_poop, 10.0f);
            isPoopSpawned = true;
        }
        else if (count != 1)
        {
            isPoopSpawned = false;
        }
    }

    public void IncreaseSpeed()
    {
        poopSpeed += 2.0f;
    }

    public void DecreaseSpeed()
    {
        poopSpeed -= 2.0f;
    }
}
