using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg5BackgroundMove : MonoBehaviour
{
    public GameObject background1; // 제일 가까운 배경
    public GameObject background2; // 그 다음 배경
    public GameObject background3; // 제일 먼 배경
    float background1Speed = 0.55f;
    float background2Speed = 0.45f;
    float background3Speed = 0.4f;
    public float minY = -3.5f;
    public float maxY = 1.7f;
    void Start()
    {
        
    }
    void Update()
    {
        background1.transform.Translate(Vector2.up * background1Speed * Time.deltaTime);
        background2.transform.Translate(Vector2.up * background2Speed * Time.deltaTime);
        background3.transform.Translate(Vector2.up * background3Speed * Time.deltaTime);
    }
}
