using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MIniGameUI : MonoBehaviour
{   
    public MiniGameManager miniGameManager;

    public Text totalJelly;
    public Text games_left;

    // Start is called before the first frame update
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>

    
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
