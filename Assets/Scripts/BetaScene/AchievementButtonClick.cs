using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementButtonClick : MonoBehaviour
{

    public GameObject sngCanvas;
    public MonoBehaviour mobileTouchScript;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AchievementCloseButtonClick()
    {
        gameObject.SetActive(false);
        sngCanvas.SetActive(true);
        mobileTouchScript.enabled = true;
    }
}
