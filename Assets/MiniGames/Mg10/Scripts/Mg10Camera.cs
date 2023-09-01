using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg10Camera : MonoBehaviour
{
    public Transform player;
    public Camera myCamera;

    public bool hit=false;

    public static Mg10Camera instance = null;

    Vector3 cameraPosition = new Vector3(0, 0, -10);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {

        if (player != null && !hit)
        {
            transform.position = player.position + cameraPosition +new Vector3(0,-3,0);
        }

    }

    public void ShakeCamera()
    {
        hit= true;
        Vibration.Instance.Vibrate();
        myCamera.transform.DOShakePosition(1f, 1f);  
    }

    public void ShakeCameraEnd()
    {
        hit = false;
    }
}
