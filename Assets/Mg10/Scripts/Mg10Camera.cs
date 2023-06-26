using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg10Camera : MonoBehaviour
{
    public Transform player;

    Vector3 cameraPosition = new Vector3(0, 0, -10);

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
       

        if (player != null)
        {
            transform.position = player.position + cameraPosition +new Vector3(0,-5,0);
        }
    }
}
