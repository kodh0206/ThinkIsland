using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Button GameStart;
    [SerializeField] 
    GameObject SNGcanvas;
    [SerializeField]
    GameObject Main;
   public void StartGame(){ 
        Main.SetActive(false);
        SNGcanvas.SetActive(true);
   }
}
