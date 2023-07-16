using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNGButtonClick : MonoBehaviour
{

    public GameObject shopCanvas;
    public GameObject profileCanvas;
    public MonoBehaviour mobileTouchScript;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShopButtonClick()
    {
        gameObject.SetActive(false);
        shopCanvas.SetActive(true);
        mobileTouchScript.enabled = false;
    }

    public void ProfileButtonClick()
    {
        gameObject.SetActive(false);
        profileCanvas.SetActive(true);
        mobileTouchScript.enabled = false;
    }
}
