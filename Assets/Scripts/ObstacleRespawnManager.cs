using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleRespawnManager : MonoBehaviour
{
    public List<GameObject> cow = new List<GameObject>();
    public GameObject[] objs;
    public int objCnt;
    // objs 배열에 있는 각 소 종류마다 objCnt개씩 생성해 cow 리스트에 추가
    void Awake()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            for (int j = 0; j < objCnt; j++)
            {
                // 생성된 소 오브젝트는 초기 상태로 비활성화
                cow.Add(CreateObj(objs[i], transform));
            }
        }
    }

    // cow 리스트에서 비활성화된 cow 오브젝트를 무작위로 선택하여 활성화
    // 일정 시간 간격을 두고 다음 cow 생성
    private void Start()
    {
        StartCoroutine(CreateCow());
    }
    IEnumerator CreateCow()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            cow[SelectDeactivateCow()].SetActive(true);
        }
    }

    // 비활성화된 소 오브젝트들 중 무작위로 하나 선택 => 인덱스 반환
    // 새로운 소 오브젝트를 생성하지 않고 기존에 비활성화된 오브젝트 재사용 가능
    int SelectDeactivateCow()
    {
        List<int> deactiveList = new List<int>();
        for (int i = 0; i < cow.Count; i++)
        {
            // 비활성화된 소 확인
            if (cow[i].activeSelf==false)
                deactiveList.Add(i);
        }

        int num = 0;
        if (deactiveList.Count > 0)
            num = deactiveList[Random.Range(0, deactiveList.Count)];

        return num;
    }

    // 인자로 받은 obj를 parent의 자식으로 생성
    // 비활성화된 상태로 반환
    GameObject CreateObj(GameObject obj, Transform parent)
    {
        GameObject copy = Instantiate(obj);
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}


