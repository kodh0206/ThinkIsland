using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue 
{
    public string name;
    [TextArea(3,10)]
     public string[] sentencesEnglish; // 영어 대화
    public string[] sentencesKorean; // 한국어 대화
}
