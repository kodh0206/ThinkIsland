using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MIniGameUI : MonoBehaviour
{   
    public MiniGameManager miniGameManager;

    public Text totalJelly;

    public Text CountDown;

    public static MIniGameUI Instance { get; private set; }
    
  
    private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
    }
    else if (Instance != this)
    {
        Destroy(gameObject);
    }
    }

    private void OnEnable()
    {
       totalJelly.text = miniGameManager.totalJelly.ToString();
       miniGameManager.minigameUIActive =true;
    }

   
  
    public void UpdateJellyText()
{
    totalJelly.text = miniGameManager.totalJelly.ToString();
}
}
