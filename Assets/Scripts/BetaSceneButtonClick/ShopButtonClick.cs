using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonClick : MonoBehaviour
{
   public GameObject sngCanvas;
    public MonoBehaviour mobileTouchScript;

    public GameObject buildingPanel; // 건물 패널
    public GameObject agriculturePanel; // 농업 패널
    public GameObject livestockPanel; // 축산 패널
    public GameObject miningPanel; // 광산 패널

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShopCloseButtonClick()
    {
        gameObject.SetActive(false);
        sngCanvas.SetActive(true);
        mobileTouchScript.enabled = true;
    }

    // 패널을 변경하는 함수들
    public void ShowBuildingPanel()
    {
        DisableAllPanels();
        buildingPanel.SetActive(true);
    }

    public void ShowAgriculturePanel()
    {
        DisableAllPanels();
        agriculturePanel.SetActive(true);
    }

    public void ShowLivestockPanel()
    {
        DisableAllPanels();
        livestockPanel.SetActive(true);
    }

    public void ShowMiningPanel()
    {
        DisableAllPanels();
        miningPanel.SetActive(true);
    }

    private void DisableAllPanels()
    {
        buildingPanel.SetActive(false);
        agriculturePanel.SetActive(false);
        livestockPanel.SetActive(false);
        miningPanel.SetActive(false);
    }
}
