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
    // objs �迭�� �ִ� �� �� �������� objCnt���� ������ cow ����Ʈ�� �߰�
    void Awake()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            for (int j = 0; j < objCnt; j++)
            {
                // ������ �� ������Ʈ�� �ʱ� ���·� ��Ȱ��ȭ
                cow.Add(CreateObj(objs[i], transform));
            }
        }
    }

    // cow ����Ʈ���� ��Ȱ��ȭ�� cow ������Ʈ�� �������� �����Ͽ� Ȱ��ȭ
    // ���� �ð� ������ �ΰ� ���� cow ����
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

    // ��Ȱ��ȭ�� �� ������Ʈ�� �� �������� �ϳ� ���� => �ε��� ��ȯ
    // ���ο� �� ������Ʈ�� �������� �ʰ� ������ ��Ȱ��ȭ�� ������Ʈ ���� ����
    int SelectDeactivateCow()
    {
        List<int> deactiveList = new List<int>();
        for (int i = 0; i < cow.Count; i++)
        {
            // ��Ȱ��ȭ�� �� Ȯ��
            if (cow[i].activeSelf==false)
                deactiveList.Add(i);
        }

        int num = 0;
        if (deactiveList.Count > 0)
            num = deactiveList[Random.Range(0, deactiveList.Count)];

        return num;
    }

    // ���ڷ� ���� obj�� parent�� �ڽ����� ����
    // ��Ȱ��ȭ�� ���·� ��ȯ
    GameObject CreateObj(GameObject obj, Transform parent)
    {
        GameObject copy = Instantiate(obj);
        copy.transform.SetParent(parent);
        copy.SetActive(false);
        return copy;
    }
}


