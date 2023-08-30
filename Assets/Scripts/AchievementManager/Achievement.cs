using UnityEngine.UI;
using UnityEngine;
[System.Serializable]
public class Achievement
{
    public string achievementName;
    public string description;
    public string id;
    public int requiredAmount;
    public int currentAmount;
    public bool unlocked;
    public Sprite achievementIcon; 
    public int level;

    public void UpdateLevel()
    {
        if(currentAmount >= 100)
        {
            level = 3;
            requiredAmount = 100;  // Level 3에 도달하면 requiredAmount는 100으로 고정됩니다.
        }
        else if(currentAmount >= 10)
        {
            level = 2;
            requiredAmount = 100;  // 다음 단계로 진행하려면 100이 필요합니다.
        }
        else if(currentAmount >= 1)
        {
            level = 1;
            requiredAmount = 10;   // 다음 단계로 진행하려면 10이 필요합니다.
        }
        else
        {
            level = 0;
            requiredAmount = 1;    // 첫 단계로 진행하려면 1이 필요합니다.
        }
    }
}



