using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public Vector2 spawnPosition = new Vector2(15, 0); // ������ ���� ��ġ
    //public GameObject obstaclePrefab; // ��ֹ� ������
    public float spawnInterval = 3f; // ��ֹ� ���� ����

    public GameObject[] clonePrefabs; // ������ �����յ�
    private GameObject clone; // ������ ������ ������Ʈ

    // 1�� ~ 2�� ���̿� ������ (cow, poop, jelly) �� �ϳ� ���� ���� (�ð��� ����)
    public float cloneSpawnDelayMin = 1f;
    public float cloneSpawnDelayMax = 2f;


    private float spawnTimer; // ��ֹ� ���� Ÿ�̸�

    void Start()
    {
        spawnTimer = spawnInterval;
        //clonePrefabs.SetActive(true);
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnObstacle();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, clonePrefabs.Length);
        GameObject clonePrefab = clonePrefabs[randomIndex];
        Instantiate(clonePrefab, spawnPosition, Quaternion.identity);
    }
}
