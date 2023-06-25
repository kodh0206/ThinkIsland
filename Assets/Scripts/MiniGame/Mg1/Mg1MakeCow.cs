using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1MakeCow : MonoBehaviour
{
    public GameObject cow;

    [SerializeField]
    private float cowSpeed = 5.0f;

    [SerializeField]
    private float time_diff = 1.5f;

    float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (time > time_diff)
        {
            GameObject new_cow = Instantiate(cow);
            new_cow.transform.position = new Vector3(15f, -0.5590893f, 0);
            new_cow.GetComponent<Mg1Cow>().SetSpeed(cowSpeed);
            time = 0;
            Destroy(new_cow, 10.0f);
        }
    }

    public void IncreaseSpeed()
    {
        cowSpeed += 2.0f;
    }
}
