using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNGislandManager : MonoBehaviour
{
   public GameObject water1;
   public GameObject field1;
   public GameObject water2;
   public GameObject field2;
   
   public GameObject water3;
   public GameObject field3;
   public GameObject water4;
   public GameObject field4;





 void Update()
    {
        // 게임 컨트롤러의 인스턴스에 접근, 예를 들어 GameController.Instance 라고 가정
        bool isLiveStockIslandUnlocked = GameController.Instance.isLiveStockIslandUnlocked;
        bool isDesertIslandUnlocked = GameController.Instance.isDesertIslandUnlocked;
        bool isWinterlandUnlocked = GameController.Instance.isWinterlandUnlocked;
        bool isTropicLandUnlocked = GameController.Instance.isTropicLandUnlocked;

        // 각 오브젝트를 활성화 또는 비활성화
        water1.SetActive(!isLiveStockIslandUnlocked);
        field1.SetActive(isLiveStockIslandUnlocked);

        water2.SetActive(!isDesertIslandUnlocked);
        field2.SetActive(isDesertIslandUnlocked);

        water3.SetActive(!isWinterlandUnlocked);
        field3.SetActive(isWinterlandUnlocked);

        water4.SetActive(!isTropicLandUnlocked);
        field4.SetActive(isTropicLandUnlocked);
    }
   

}
