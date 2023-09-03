using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LanguageText : MonoBehaviour
{   
    public TextMeshProUGUI Text;
    public string KoreanText;
    public string EnglishText;
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
            Text.text = KoreanText;
        }
        else
        {
            Text.text=EnglishText;
        }
    }
}
