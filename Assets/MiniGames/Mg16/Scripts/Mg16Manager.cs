using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16Manager : MonoBehaviour
{
    public int score = 0;
    private Mg16Fish1 mg16Fish1;
    private Mg16Fish2 mg16Fish2;
    private Mg16FishSpawner mg16FishSpawner;
    private Mg16Jelly mg16Jelly;
    private Mg16JellySpawner mg16JellySpawner;

    private Mg16Player mg16Player;

    private void Start()
    {
        mg16Fish1 = FindObjectOfType<Mg16Fish1>();
        mg16Fish2 = FindObjectOfType<Mg16Fish2>();
        mg16FishSpawner = FindObjectOfType<Mg16FishSpawner>();
        mg16Jelly = FindObjectOfType<Mg16Jelly>();
        mg16JellySpawner = FindObjectOfType<Mg16JellySpawner>();
        mg16Player = FindObjectOfType<Mg16Player>();
    }

    private void Update()
    {
        
    }

    public void AddScore()
    {
        score += 1;
        if (score % 5 == 0)
        {
            if (mg16Fish1 != null)
            {
                mg16Fish1.SpeedTime();
                mg16Fish2.SpeedTime();
                mg16FishSpawner.SpeedTime();
                mg16Jelly.SpeedTime();
                mg16JellySpawner.SpeedTime();
                mg16Player.SpeedTime();
            }
        }
    }
}
