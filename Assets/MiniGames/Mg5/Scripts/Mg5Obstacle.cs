using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg5Obstacle : MonoBehaviour
{
    [SerializeField]
    private float obstacleSpeed = 2.0f;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        // ·£´ýÇÑ ¹æÇâ º¤ÅÍ »ý¼º
        moveDirection = new Vector3(Random.Range(-0.8f, 0.8f), Random.Range(0, 0.8f), 0f).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * obstacleSpeed * Time.deltaTime;
    }

    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }

}
