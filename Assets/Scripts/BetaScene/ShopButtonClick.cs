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

    void Start()
    {

        // 싱글톤 초기화
        gameController = GameObject.Find("GameManager").GetComponent<GameController>();

        // 상점 위 돈, 젤리, 에너지와 현 돈, 젤리, 에너지 연동
        money.text = gameController.curentgold.ToString();
        jelly.text = gameController.currentjellyCount.ToString();
        energy.text = gameController.currentActionPoints.ToString();

    }

    void Update()
    {
        
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
    }

    public void ShowAgriculturePanel()
    { AudioManager.Instance.PlayPressed();
        DisableAllPanels();
        agriculturePanel.SetActive(true);
    }

    public void ShowLivestockPanel()
    { AudioManager.Instance.PlayPressed();
        DisableAllPanels();
        livestockPanel.SetActive(true);
    }

    public void ShowMiningPanel()
    { AudioManager.Instance.PlayPressed();
        DisableAllPanels();
        miningPanel.SetActive(true);
    }

    private void DisableAllPanels()
    { AudioManager.Instance.PlayPressed();
        buildingPanel.SetActive(false);
        agriculturePanel.SetActive(false);
        livestockPanel.SetActive(false);
        miningPanel.SetActive(false);
    }
}
