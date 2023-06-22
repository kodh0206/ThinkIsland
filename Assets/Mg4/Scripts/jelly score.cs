using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jellyscore : MonoBehaviour

   
{
    public static int jelly_Score = 0;
// Start is called before the first frame update
void Start()
    {
        jelly_Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = jelly_Score.ToString();
    }

    public void addjellyScore()
    {
        jelly_Score += 1;
    }

}
