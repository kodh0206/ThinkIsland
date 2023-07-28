using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedCropCollection : MonoBehaviour
{
    public GameObject[] cropObjects;
    
    //unlockedCropsLength = GameController.Instance.currentUnlockedCrops.Count;


    void Start()
    {
        for (int i = 0; i < GameController.Instance.currentUnlockedCrops.Count; i++)
        {
            cropObjects[i].SetActive(true);
        }
    }
}
