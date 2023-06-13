using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CharacterSaveData
{
    public string characterName;
    public int index;
    public int miniGameIndex;
    // 추가적인 캐릭터 데이터 필드들을 여기에 추가할 수 있습니다.

    public CharacterSaveData()
    {
        characterName = "";
        index = 0;
        miniGameIndex = 0;
    }
}

