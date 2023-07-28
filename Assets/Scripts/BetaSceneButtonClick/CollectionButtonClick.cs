using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionButtonClick : MonoBehaviour
{

    public GameObject sngCanvas;
    public GameObject cropCollection;
    public GameObject characterCollection;

    // 페이지 넘김(1p, 2p)
    public GameObject page1Collection;
    public GameObject page2Collection;

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
        characterCollection.SetActive(false);

        // 페이지 초기화
        page1Collection.SetActive(true);
        page2Collection.SetActive(false);
    }

    public void CropCollectionButtonClick()
    {
        cropCollection.SetActive(true);
        characterCollection.SetActive(false);
    }

    public void characterCollectionButtonClick()
    {
        cropCollection.SetActive(false);
        characterCollection.SetActive(true);
    }


    // 페이지 넘김 기능 구현
    public void page1CollectionButtonClick()
    {
        page1Collection.SetActive(true);
        page2Collection.SetActive(false);
    }

    public void page2CollectionButtonClick()
    {
        page1Collection.SetActive(false);
        page2Collection.SetActive(true);
    }
}
