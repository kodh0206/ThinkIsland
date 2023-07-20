using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg18MovingBack : MonoBehaviour
{
    public float movespeed = 5f;

    [SerializeField] float posValue;

    Vector2 startPos;
    float newPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos= transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = Mathf.Repeat(Time.time * movespeed, posValue);
        transform.position = startPos + Vector2.left *newPos;
    }

    

    public void IncreaseSpeed()
    {
        movespeed += 1.0f; // 장애물의 스피드 증가
        
    }
    public void DecreaseSpeed()
    {
        movespeed -= 1.0f; // 장애물의 스피드 증가

    }
}
