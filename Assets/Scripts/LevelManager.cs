using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{   
    public int level = 1;
    public int exp = 0;

    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }
    public int[] expToLevelUp = {5, 37, 
        60, 78, 92,104,115,124,133,140,
        147,154,159,165,170,175,175,179,184,
        188,192,195,199,202,205,209,212,214,
        217,220,222,225,227,230,232,234,237,239,241,
        243,245,247,248,250,252,254,256,257,259,260,315}; // 각 레벨별 필요한 경험치
    
    
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update

        public void GainExp(int amount)
    {
        exp += amount;
        
        // 레벨 업을 할 수 있는지 확인
        if (level < expToLevelUp.Length && exp >= expToLevelUp[level-1]) 
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        exp = 0; // 경험치를 0으로 리셋 (또는 누적 방식으로 구현 가능)

        UnlockFeatures(level);
    }

    public void UnlockFeatures(int level)
    {
        switch(level)
        {
            case 3:
                // 레벨 3에 해금되는 기능 구현
                break;
            case 5:
                break;

            case 7:
                // 레벨 3에 해금되는 기능 구현
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                // 레벨 2에 해금되는 기능 구현
                break;
            case 11:
                // 레벨 3에 해금되는 기능 구현
                break;
            case 4:
                break;


            
            // 그 외 레벨에 따른 기능 구현
        }
    }


}





 

