using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MiniGameTrain : MonoBehaviour
{
    public Button play1;
    MiniGameManager miniGame;


    void Awake()
    {
        miniGame =GameObject.Find("MiniGameManager").GetComponent<MiniGameManager>();
    }

    private void Start()
    {
        play1.onClick.AddListener(miniGame.StartMiniGame);

    }   
    void gotoGame1()
    {   
    }

  

    void ExitGame()
    {
        Application.Quit();
    }
}
