using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mg1JellyScore : MonoBehaviour

   
{
    public static int jelly_Score = 0;
void Start()
    {
        jelly_Score = 0;
    }

    void Update()
    {
        GetComponent<Text>().text = jelly_Score.ToString();
    }

    public void addjellyScore()
    {   
        //MiniGameManager.Instance.AddJelly();
        jelly_Score += 1;
    }

}
