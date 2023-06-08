using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleRespawnManager : MonoBehaviour
{
    public List<GameObject> cow = new List<GameObject>();
    public GameObject[] cows;
    public int cowsCnt = 1;
    void Awake()
    {
        for (int i = 0; i < cows.Length; i++)
        {
            for (int j = 0; j < cowsCnt; j++)
            {
                cow.Add(CreateCows(cows[i], transform));
            }
        }
    }

    private void Start()
    {
        StartCoroutine(CreateCows());
    }

    IEnumerator CreateCows()
    {
        while (true)
        {
            cow[SelectDeactivateCows()].SetActive(true);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    int SelectDeactivateCows()
    {
        List<int> deactiveList = new List<int>();
        for (int i = 0; i < cow.Count; i++)
        {
            // 비활성화된 소 확인
            if (cow[i].activeSelf)
                deactiveList.Add(i);
        }

        int num = 0;
        if (deactiveList.Count > 0)
            num = deactiveList[Random.Range(0, deactiveList.Count)];

        return num;
    }

    GameObject CreateCows(GameObject cow, Transform parent)
    {
        GameObject copy = Instantiate(cow);
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}


