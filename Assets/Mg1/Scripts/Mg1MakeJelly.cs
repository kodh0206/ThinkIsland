using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1MakeJelly : MonoBehaviour
{
    public GameObject jelly;

    [SerializeField]
    private float jellySpeed = 5.0f;

    [SerializeField]
    private float time_diff = 1.5f;

    float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (time > time_diff)
        {
            GameObject new_jelly = Instantiate(jelly);
            new_jelly.transform.position = new Vector3(15f, Random.Range(-0.5590893f, 1.5f), 0);
            new_jelly.GetComponent<Mg1Jelly>().SetSpeed(jellySpeed);
            time = 0;
            Destroy(new_jelly, 10.0f);
        }
    }

    public void IncreaseSpeed()
    {
        jellySpeed += 2.0f;
    }
}
