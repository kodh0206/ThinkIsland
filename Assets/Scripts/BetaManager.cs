using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class BetaManager : MonoBehaviour
{
    public Button play1;
    MiniGameManager miniGame;
    public Button exit;
    GameController gameController;
    public Button RadioButton;

    public TextMeshProUGUI   money;
    public TextMeshProUGUI  jelly;
    public TextMeshProUGUI energy;
    void Awake()
    {
        miniGame =GameObject.Find("MiniGameManager").GetComponent<MiniGameManager>();
        gameController = GameObject.Find("GameManager").GetComponent<GameController>();
    }

    private void Start()
    {
        play1.onClick.AddListener(StartMiniGame);
        exit.onClick.AddListener(ExitGame);
        RadioButton.onClick.AddListener(gotoRadio);
        money.text = gameController.curentgold.ToString();
        jelly.text = gameController.currentjellyCount.ToString();
        energy.text =gameController.currentActionPoints.ToString();

    }   
    
    void StartMiniGame()
    {   play1.interactable=false;//여러 클릭 방지 
        AudioManager.Instance.StartMiniGame();
        miniGame.StartMiniGameWithAudio();
       
    }
    void gotoRadio()
    {
        SceneManager.LoadScene("RadioScene");
    }

  

    void ExitGame()
    {
        Application.Quit();
    }
}
