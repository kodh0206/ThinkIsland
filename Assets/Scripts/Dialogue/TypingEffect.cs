using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingEffect : MonoBehaviour
{   

    public int CharPerSeconds;
    public GameObject EndCursor;

    string targetMsg;
    TextMeshProUGUI msgText;
    private int index;
    // Start is called before the first frame update
    private void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
    }
    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }

   

    void EffectStart()
    {
        msgText.text = "";
        index =0;

        Invoke("Effecting",1/CharPerSeconds);
    }

    void Effecting()
    {   
        if(msgText.text == targetMsg)
        {   
            EffectEnd();
            return;
        }
        msgText.text +=targetMsg[index];
        index++;
        Invoke("Effecting",1/CharPerSeconds);
    }

    void EffectEnd()
    {

    }
}
