using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNGButtonClick : MonoBehaviour
{

    public GameObject shopCanvas;
    public GameObject profileCanvas;
    public GameObject collectionCanvas;
    public GameObject AchievementCanvas;
    public GameObject OptionCanvas;
    public MonoBehaviour mobileTouchScript;
 

    public void ShopButtonClick()
    {
        gameObject.SetActive(false);
        shopCanvas.SetActive(true);
        mobileTouchScript.enabled = false;
        Debug.Log(  mobileTouchScript.enabled = false);
    }

    public void ProfileButtonClick()
    {
        gameObject.SetActive(false);
        profileCanvas.SetActive(true);
        mobileTouchScript.enabled = false;
        Debug.Log(  mobileTouchScript.enabled = false);
    }

    public void CollectionButtonClick()
    {
        gameObject.SetActive(false);
        collectionCanvas.SetActive(true);
        mobileTouchScript.enabled = false;
        Debug.Log(  mobileTouchScript.enabled = false);
    }

    public void AchievementButtonClick()
    {
        gameObject.SetActive(false);
        AchievementCanvas.SetActive(true);
        mobileTouchScript.enabled = false;
        Debug.Log(  mobileTouchScript.enabled = false);
    }


    public void OptionButtonClick()
    {
        gameObject.SetActive(false);
        OptionCanvas.SetActive(true);
        mobileTouchScript.enabled = false;
        Debug.Log(  mobileTouchScript.enabled = false);
    }
}
