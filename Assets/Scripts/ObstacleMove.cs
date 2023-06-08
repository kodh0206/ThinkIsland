using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public Vector2 spawnPosition = new Vector2(15, 0); // 복제본 생성 위치
    //public GameObject obstaclePrefab; // 장애물 프리팹
    public float spawnInterval = 3f; // 장애물 생성 간격

    public GameObject[] clonePrefabs; // 복제본 프리팹들
    private GameObject clone; // 생성된 복제본 오브젝트

    // 1초 ~ 2초 사이에 복제본 (cow, poop, jelly) 중 하나 랜덤 생성 (시간도 랜덤)
    public float cloneSpawnDelayMin = 1f;
    public float cloneSpawnDelayMax = 2f;


    private float spawnTimer; // 장애물 생성 타이머

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
