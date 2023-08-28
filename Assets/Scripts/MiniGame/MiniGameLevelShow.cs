using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameLevelShow : MonoBehaviour
{


    public Sprite Level;
    public Sprite[] LevelSprite = new Sprite[4];
    public static MiniGameLevelShow Instance { get; private set; }

    public Image Changesprite;

    // 클래스의 다른 멤버 변수나 함수 등...

    private void Awake()
    {
        // Instance 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Changesprite = GetComponent<Image>();
        Changesprite.enabled = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNowLevel(int difficulty)
    {
        Debug.Log("레벨스프라이트변경시작2");
        StartCoroutine(ChangeSprite(difficulty));
        Debug.Log("레벨스프라이트변경끝");
    }

    private IEnumerator ChangeSprite(int difficulty)
    {
        Debug.Log("레벨스프라이트변경시작3");

        Changesprite.sprite = LevelSprite[difficulty];
        Changesprite.enabled = true; // 이미지를 활성화하여 보이도록 함

        yield return new WaitForSeconds(1f);

        Changesprite.enabled = false; // 이미지를 비활성화하여 숨김
    }

}
