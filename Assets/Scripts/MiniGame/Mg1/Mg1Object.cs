using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1Object : MonoBehaviour
{
    [SerializeField]
    protected float objectSpeed = 5.0f;

    public void SetSpeed(float speed)
    {
        objectSpeed = speed;
    }
}
