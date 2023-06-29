using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BetaManager : MonoBehaviour
{
    public Button play1;
    MiniGameManager miniGame;
    public Button exit;

    void Awake()
    {
        miniGame =GameObject.Find("MiniGameManager").GetComponent<MiniGameManager>();
    }

    private void Start()
    {
        play1.onClick.AddListener(miniGame.StartMiniGame);
        exit.onClick.AddListener(ExitGame);
    }   
    void gotoGame1()
    {   
    }

  

    void ExitGame()
    {
        Application.Quit();
    }
}