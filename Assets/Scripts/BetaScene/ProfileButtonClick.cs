using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileButtonClick : MonoBehaviour
{

    public GameObject sngCanvas;
    public MonoBehaviour mobileTouchScript;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ProfileCloseButtonClick()
    {   AudioManager.Instance.PlayPressed();
        gameObject.SetActive(false);
        sngCanvas.SetActive(true);
        mobileTouchScript.enabled = true;
    }
}
