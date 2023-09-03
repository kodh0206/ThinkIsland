using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LanguageUI : MonoBehaviour
{   public Image someImage; // 언어에 따라 변경될 이미지
    public Sprite koreanSprite; // 한국어일 때의 스프라이트
    public Sprite englishSprite; // 영어일 때의 스프라이트

    // Start is called before the first frame update
  void Start()
    {
        UpdateLanguageUI();
        
    }

   void Update()
   {
    UpdateLanguageUI();
   }
    void UpdateLanguageUI()
    {
        if (GameController.Instance.isKorean)
        {
            someImage.sprite = koreanSprite;
        }
        else
        {
            someImage.sprite = englishSprite;
        }
    }
}
