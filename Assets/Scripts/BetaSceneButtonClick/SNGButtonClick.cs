using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNGButtonClick : MonoBehaviour
{

    public GameObject shopCanvas;
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
}
