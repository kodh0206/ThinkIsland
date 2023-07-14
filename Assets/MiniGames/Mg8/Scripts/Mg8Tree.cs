using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg8Tree : MonoBehaviour
{

    [SerializeField]
    public float obstacleSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * obstacleSpeed * Time.deltaTime;
    }

    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }

}
