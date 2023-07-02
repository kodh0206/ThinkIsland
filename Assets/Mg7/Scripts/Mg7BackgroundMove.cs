using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg7BackgroundMove : MonoBehaviour
{
    public GameObject background1; // 제일 가까운 배경
    public GameObject background2; // 그 다음 배경
    public GameObject background3; // 제일 먼 배경
    float background1Speed = 0.5f;
    float background2Speed = 0.3f;
    float background3Speed = 0.1f;
    public float minY = -3.5f;
    public float maxY = 1.7f;
    void Start()
    {
        
    }
    void Update()
    {
        background1.transform.Translate(Vector2.down * background1Speed * Time.deltaTime);
        background2.transform.Translate(Vector2.down * background2Speed * Time.deltaTime);
        background3.transform.Translate(Vector2.down * background3Speed * Time.deltaTime);
    }
}
