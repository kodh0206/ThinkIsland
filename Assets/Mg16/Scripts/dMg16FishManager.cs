using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dMg16FishManager : MonoBehaviour
{
    public dMg16Fish mg16Fish;
    void Start()
    {
        mg16Fish = FindObjectOfType<dMg16Fish>();
    }

    public void IncreaseSpeed()
    {
        if (mg16Fish.fishSpeed <= 10.0f)
        {
            mg16Fish.fishSpeed += 2.0f;
        }
        else
        {
            mg16Fish.fishSpeed = 10.0f;
        }   
    }
}
