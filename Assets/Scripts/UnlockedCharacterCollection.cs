using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedCharacterCollection : MonoBehaviour
{

    // Mg1Character부터 Mg20Character까지 순서대로 저장
    public GameObject[] characterObjects;
    int i = 1; //
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < characterObjects.Length; i++)
        {
            // unlockedMiniGames 리스트 안에 "Mg1"이 있는지 확인
            string characterName = "Mg" + (i + 1);
            if (GameController.Instance.unlockedMiniGames.Contains(characterName))
            {
                characterObjects[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // unlockedMiniGames 리스트 안에 "Mg1"이 있는지 확인
        if (GameController.Instance.unlockedMiniGames.Contains("Mg"+i))
        {
            //Mg1Character.SetActive(true);
        }
    }
}