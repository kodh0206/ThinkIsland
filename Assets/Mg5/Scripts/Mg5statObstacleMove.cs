using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg5statObstacleMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float obstacleSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * obstacleSpeed * Time.deltaTime;
    }
}
