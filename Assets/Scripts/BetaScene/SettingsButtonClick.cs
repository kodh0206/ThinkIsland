using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettingsButtonClick : MonoBehaviour
{
    public GameObject sngCanvas;
    public MonoBehaviour mobileTouchScript;

    public Toggle bgmToggle;
    public Toggle sfxToggle;
    public Toggle vibeToggle;
     public Toggle languageToggle;
    public Sprite selectedSprite; // Selected 상태의 스프라이트//꺼져있음
    public Sprite defaultSprite;  // Default 상태의 스프라이트 //켜져있음
    void Start()
    {

    bool isBGMOn = AudioManager.Instance.isBGMOn;
    bool isSFXOn = AudioManager.Instance.isSFXOn;
    bool isVibeOn =Vibration.Instance.isVibrate;
    bool isKorean =GameController.Instance.isKorean;
    bgmToggle.isOn = !isBGMOn;
    sfxToggle.isOn = !isSFXOn;
    vibeToggle.isOn = !isVibeOn;
    languageToggle.isOn = !isKorean;

        // 토글 값 변경 리스너 추가
    languageToggle.onValueChanged.AddListener(OnLanguageToggleChanged);
    // 토글의 스프라이트 이미지도 업데이트합니다.
    UpdateToggleImage(bgmToggle, !isBGMOn);
    UpdateToggleImage(sfxToggle, !isSFXOn);
    UpdateToggleImage(vibeToggle, !isVibeOn);
    UpdateToggleImage(languageToggle, !isKorean);
    bgmToggle.onValueChanged.AddListener(OnBGMToggleChanged);
    sfxToggle.onValueChanged.AddListener(OnSFXToggleChanged);
    vibeToggle.onValueChanged.AddListener(OnVibeToggleChanged);
    languageToggle.onValueChanged.AddListener(OnLanguageToggleChanged);
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
{    Debug.Log("BGM Toggle value changed to: " + isOn);
    UpdateToggleImage(bgmToggle, isOn);
    AudioManager.Instance.PlayOnOff();
    if (isOn)
    {   
        Debug.Log("BGM 꺼짐");
        AudioManager.Instance.isBGMOn = false;
        PlayerPrefs.SetInt("BGM", 0);
    }
    else
    {
        Debug.Log("BGM 켜짐");
        AudioManager.Instance.isBGMOn = true;
        PlayerPrefs.SetInt("BGM", 1);
    }

    // BGM 상태에 따라서 재생/정지 처리를 합니다.
    if (AudioManager.Instance.isBGMOn)
        AudioManager.Instance.PlayBGM();
    else
        AudioManager.Instance.StopBGM();
}


    public void OnSFXToggleChanged(bool isOn)
    {

        AudioManager.Instance.PlayOnOff();
        Debug.Log("SFX Toggle value changed to: " + isOn);
        UpdateToggleImage(sfxToggle, isOn);

        AudioManager.Instance.isSFXOn = !isOn; 
    }

    public void OnVibeToggleChanged(bool isOn)
    {
        AudioManager.Instance.PlayOnOff();
        Debug.Log("Vibe Toggle value changed to: " + isOn);
        UpdateToggleImage(vibeToggle, isOn);
        Vibration.Instance.isVibrate =!isOn;
    }
    public void SettingCloseButtonClick()
    {   AudioManager.Instance.PlayPressed();
        gameObject.SetActive(false);
        sngCanvas.SetActive(true);
        mobileTouchScript.enabled = true;
    }
    


    // 언어 설정


    public void OnLanguageToggleChanged(bool isOn)
    {
       AudioManager.Instance.PlayOnOff();
        Debug.Log("Vibe Toggle value changed to: " + isOn);
        UpdateToggleImage(languageToggle, isOn);
        GameController.Instance.isKorean =!isOn;
        // 필요한 경우, UI를 갱신하는 메소드를 
        
    }
}







