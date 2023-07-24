using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionButtonClick : MonoBehaviour
{

    public GameObject sngCanvas;
    public GameObject cropCollection;
    public GameObject animalCollection;
    public MonoBehaviour mobileTouchScript;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CollectionCloseButtonClick()
    {
        gameObject.SetActive(false);
        sngCanvas.SetActive(true);
        mobileTouchScript.enabled = true;

        // 캔버스 초기화
        cropCollection.SetActive(true);
        animalCollection.SetActive(false);
    }

    public void CropCollectionButtonClick()
    {
        cropCollection.SetActive(true);
        animalCollection.SetActive(false);
    }

    public void AnimalCollectionButtonClick()
    {
        cropCollection.SetActive(false);
        animalCollection.SetActive(true);
    }
}
