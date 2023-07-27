using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{

    // Start is called before the first frame update
    public List<GameObject> characterList;
    public int currentCharacterIndex = 0; // 현재 선택된 캐릭터 인덱스

    void Start()
    {
        ChangeCharacter();
    }

 
    // 현재 선택된 캐릭터를 변경하는 함수
    public void ChangeCharacter()
    {
        // 유효한 인덱스인지 확인합니다.
      int index = GameController.Instance.unlockedMiniGames.Count;

      for(int i=0; i<index; i++)
      {
        characterList[index].SetActive(true);
      }
    }
}

