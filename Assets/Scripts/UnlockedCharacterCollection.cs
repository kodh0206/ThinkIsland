using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedCharacterCollection : MonoBehaviour
{

    // Mg1Character부터 Mg20Character까지 순서대로 저장
    public GameObject[] characterObjects;
    int i = 1;

    
    void Start()
    {

        for (int i = 0; i < characterObjects.Length; i++)
        {
            string characterName = "Mg" + (i + 1);
            if (GameController.Instance.unlockedMiniGames.Contains(characterName))
            {
                characterObjects[i].SetActive(true);
            }
        }
    }
}