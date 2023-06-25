using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1MakePoop : MonoBehaviour
{
    public GameObject poop;

    [SerializeField]
    private float poopSpeed = 5.0f;

    [SerializeField]
    private float time_diff = 1.5f;

    float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (time > time_diff)
        {
            GameObject new_poop = Instantiate(poop);
            new_poop.transform.position = new Vector3(15f, -0.5590893f, 0);
            new_poop.GetComponent<Mg1Poop>().SetSpeed(poopSpeed);
            time = 0;
            Destroy(new_poop, 10.0f);
        }
    }

    public void IncreaseSpeed()
    {
        poopSpeed += 2.0f;
    }
}
