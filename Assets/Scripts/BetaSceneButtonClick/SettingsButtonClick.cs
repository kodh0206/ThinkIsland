using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsButtonClick : MonoBehaviour
{
    public GameObject sngCanvas;
    public MonoBehaviour mobileTouchScript;

    public Toggle bgmToggle;
    public Toggle sfxToggle;
    public Toggle vibeToggle;
    
    public Sprite selectedSprite; // Selected 상태의 스프라이트//꺼져있음
    public Sprite defaultSprite;  // Default 상태의 스프라이트 //켜져있음
    void Start()
    {
        bgmToggle.onValueChanged.AddListener(OnBGMToggleChanged);
        sfxToggle.onValueChanged.AddListener(OnSFXToggleChanged);
    }
 void UpdateToggleImage(Toggle toggle, bool isOn)
    {
        if(isOn)
        {
            toggle.targetGraphic.GetComponent<Image>().sprite = selectedSprite;
        }
        else
        {
            toggle.targetGraphic.GetComponent<Image>().sprite = defaultSprite;
        }
    }
    
    public void OnBGMToggleChanged(bool isOn)
{   
      Debug.Log("BGM Toggle value changed to: " + isOn);
      UpdateToggleImage(bgmToggle, isOn);

    if (isOn)
    {   
        Debug.Log("BGM꺼짐 ");
        // 배경음 켜기
        //PlayBGM();
        //PlayerPrefs.SetInt("BGM", 1);
    }
    else
    {   
        Debug.Log("BGM켜짐 ");
        // 배경음 끄기
        //StopBGM();
        //PlayerPrefs.SetInt("BGM", 0);
    }
}

    public void OnSFXToggleChanged(bool isOn)
    {

  Debug.Log("SFX Toggle value changed to: " + isOn);
    // 효과음의 상태에 따라 설정값을 변경하며,
    // 필요한 경우 효과음을 위한 별도의 오디오 제어 로직을 추가합니다.
    UpdateToggleImage(sfxToggle, isOn);

    
     if (isOn)
    {   
        Debug.Log("효과음꺼짐 ");
        // 배경음 켜기
        //PlayBGM();
        //PlayerPrefs.SetInt("BGM", 1);
    }
    else
    {   
        Debug.Log("효과음 켜짐 ");
        // 배경음 끄기
        //StopBGM();
        //PlayerPrefs.SetInt("BGM", 0);
    }
    }
    public void SettingCloseButtonClick()
    {
        gameObject.SetActive(false);
        sngCanvas.SetActive(true);
        mobileTouchScript.enabled = true;
    }
}
