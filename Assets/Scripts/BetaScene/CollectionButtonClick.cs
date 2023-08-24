using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionButtonClick : MonoBehaviour
{

    public GameObject sngCanvas;
    public GameObject cropCollection;
    public GameObject characterCollection;


    // 캐릭터 해금 여부
    public GameObject unlockedCharacter19;

    // 페이지 넘김(1p, 2p)
    public GameObject page1Collection;
    public GameObject page2Collection;

    // 버튼
    public GameObject pageButton;

    public MonoBehaviour mobileTouchScript;
    void Start()
    {
        
    }

    void Update()
    {
        // 19번째 캐릭터가 해금되어 있으면
        if (unlockedCharacter19.activeSelf)
        {
            // 버튼 활성화
            pageButton.SetActive(true);
        }
        else
        {
            // 버튼 비활성화
            pageButton.SetActive(false);
        }

        Debug.Log(GameController.Instance.level);
    }

    public void CollectionCloseButtonClick()
    {   
         AudioManager.Instance.PlayPressed();
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
    {    AudioManager.Instance.PlayPressed();
        cropCollection.SetActive(true);
        characterCollection.SetActive(false);
    }

    public void characterCollectionButtonClick()
    {  AudioManager.Instance.PlayPressed();
        cropCollection.SetActive(false);
        characterCollection.SetActive(true);
    }


    // 페이지 넘김 기능 구현
    public void page1CollectionButtonClick()
    {   AudioManager.Instance.PlayPage();
        page1Collection.SetActive(true);
        page2Collection.SetActive(false);
    }

    public void page2CollectionButtonClick()
    {    AudioManager.Instance.PlayPage();
        page1Collection.SetActive(false);
        page2Collection.SetActive(true);
    }
}
