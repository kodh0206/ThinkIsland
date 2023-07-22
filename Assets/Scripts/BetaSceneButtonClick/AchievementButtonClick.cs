using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementButtonClick : MonoBehaviour
{

    public GameObject farmAchievementCanvas;
    public GameObject animalAchievementCanvas;
    public GameObject sngCanvas;
    public MonoBehaviour mobileTouchScript;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void FarmAchievementCloseButtonClick()
    {
        gameObject.SetActive(false);
        farmAchievementCanvas.SetActive(true); // 초기 세팅
        animalAchievementCanvas.SetActive(false);
        sngCanvas.SetActive(true);
        mobileTouchScript.enabled = true;
    }

    public void AnimalAchievementCloseButtonClick()
    {
        gameObject.SetActive(false);
        farmAchievementCanvas.SetActive(true); // 초기 세팅
        animalAchievementCanvas.SetActive(false);
        sngCanvas.SetActive(true);
        mobileTouchScript.enabled = true;
    }

    public void FarmAchievementButtonClick()
    {
        farmAchievementCanvas.SetActive(true);
        animalAchievementCanvas.SetActive(false);
        //mobileTouchScript.enabled = false;
    }

    public void AnimalAchievementButtonClick()
    {
        animalAchievementCanvas.SetActive(true);
        farmAchievementCanvas.SetActive(false);
        //mobileTouchScript.enabled = false;
    }
}
