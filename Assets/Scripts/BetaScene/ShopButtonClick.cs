using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButtonClick : MonoBehaviour
{
    public GameObject sngCanvas;
    public MonoBehaviour mobileTouchScript;
    public GameController gameController;

    public GameObject buildingPanel; // 건물 패널
    public GameObject agriculturePanel; // 농업 패널
    public GameObject livestockPanel; // 축산 패널
    public GameObject miningPanel; // 광산 패널

    // 상점 위 텍스트
    public TextMeshProUGUI money;
    public TextMeshProUGUI jelly;
    public TextMeshProUGUI energy;

    public Button shopButton;
    public Button farmButton;
    public Button animalButton;
    public Button buildButton;

    void Start()
    {
    }

    void Update()
    {
        // 상점 위 돈, 젤리, 에너지와 현 돈, 젤리, 에너지 연동
        money.text = GameController.Instance.curentgold.ToString();
        jelly.text = GameController.Instance.currentjellyCount.ToString();
        energy.text = GameController.Instance.currentActionPoints.ToString();
    }

    public void ShopCloseButtonClick()
    {    AudioManager.Instance.PlayPressed();
        gameObject.SetActive(false);
        sngCanvas.SetActive(true);
        mobileTouchScript.enabled = true;
    }

    // 패널을 변경하는 함수들
    public void ShowBuildingPanel()
    {    AudioManager.Instance.PlayPressed();
        DisableAllPanels();
        buildingPanel.SetActive(true);

        // 패널이 활성화되면 알파 값을 1로 설정 (완전 불투명)
        SetButtonAlpha(buildButton, 1f);
    }

    public void ShowAgriculturePanel()
    { AudioManager.Instance.PlayPressed();
        DisableAllPanels();
        agriculturePanel.SetActive(true);

        // 패널이 활성화되면 알파 값을 1로 설정 (완전 불투명)
        SetButtonAlpha(farmButton, 1f);
    }

    public void ShowLivestockPanel()
    { AudioManager.Instance.PlayPressed();
        DisableAllPanels();
        livestockPanel.SetActive(true);

        // 패널이 활성화되면 알파 값을 1로 설정 (완전 불투명)
        SetButtonAlpha(animalButton, 1f);
        
    }

    public void ShowMiningPanel()
    { AudioManager.Instance.PlayPressed();
        DisableAllPanels();
        miningPanel.SetActive(true);

        // 패널이 활성화되면 알파 값을 1로 설정 (완전 불투명)
        SetButtonAlpha(shopButton, 1f);

    }

    private void DisableAllPanels()
    { AudioManager.Instance.PlayPressed();
        buildingPanel.SetActive(false);
        agriculturePanel.SetActive(false);
        livestockPanel.SetActive(false);
        miningPanel.SetActive(false);

        // 패널이 비활성화되면 알파 값을 0.72로 설정 (약간 투명)
        SetButtonAlpha(shopButton, 0.72f);
        SetButtonAlpha(farmButton, 0.72f);
        SetButtonAlpha(animalButton, 0.72f);
        SetButtonAlpha(buildButton, 0.72f);
    }

    // 불투명도 조절 함수
    private void SetButtonAlpha(Button button, float alpha)
{
    Image buttonImage = button.GetComponent<Image>();
    if (buttonImage != null)
    {
        Color buttonColor = Color.white; // 기본 색상을 흰색으로 초기화

        if (alpha == 0.72f) // 알파 값이 0.72일 때
        {
            // D4D4D4 (약간 투명한 회색) 색상을 할당
            buttonColor = new Color(0.831f, 0.831f, 0.831f, alpha);
        }
        else if (alpha == 1f) // 알파 값이 1일 때
        {
            // FFFFFF (흰색) 색상을 할당
            buttonColor = new Color(1f, 1f, 1f, alpha);
        }

        buttonImage.color = buttonColor;
    }
}
}
