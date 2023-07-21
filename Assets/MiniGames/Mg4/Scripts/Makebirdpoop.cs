using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Makebirdpoop : MonoBehaviour
{
    public GameObject birdpoop;
    public int minNumPoopsToSpawn = 1; // 최소 생성 배설물 개수
    public int maxNumPoopsToSpawn = 1; // 최대 생성 배설물 개수

    [SerializeField]
    private float birdpoopSpeed = 5.0f; // 배설물의 초기 속도

    [SerializeField]
    public float minTimeDiff = 1.0f; // 최소 생성 간격
    [SerializeField]
    public float maxTimeDiff = 2.0f; // 최대 생성 간격

    float time = 0;
    float timeDiff = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomTimeDiff();
        StartCoroutine(SpawnBirdPoops()); // 코루틴 시작
    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;
        //if (time > timeDiff)
        //{
        //    int numPoopsToSpawn = Random.Range(minNumPoopsToSpawn, maxNumPoopsToSpawn + 1);

        //    for (int i = 0; i < numPoopsToSpawn; i++)
        //    {
        //        GameObject new_birdpoop = Instantiate(birdpoop);
        //        new_birdpoop.transform.position = new Vector3(10.5f, Random.Range(-1.6f, 3.7f), 0);
        //        new_birdpoop.GetComponent<birdpoop>().SetSpeed(birdpoopSpeed);
        //        Destroy(new_birdpoop, 10.0f);
        //    }

        //    time = Random.Range(0f, minTimeDiff);
        //    SetRandomTimeDiff(); // 다음 생성 간격을 랜덤으로 설정
        //}
    }

    private IEnumerator SpawnBirdPoops()
    {
        while (true)
        {
            int numPoopsToSpawn = Random.Range(minNumPoopsToSpawn, maxNumPoopsToSpawn + 1);

            for (int i = 0; i < numPoopsToSpawn; i++)
            {
                GameObject new_birdpoop = Instantiate(birdpoop);
                new_birdpoop.transform.position = new Vector3(10.5f, Random.Range(-1.6f, 3.7f), 0);
                new_birdpoop.GetComponent<birdpoop>().SetSpeed(birdpoopSpeed);
                Destroy(new_birdpoop, 10.0f);
                yield return new WaitForSeconds(0.2f); // 텀을 두기 위해 WaitForSeconds 호출
            }

            
       
        }
    }

    void SetRandomTimeDiff()
    {
        timeDiff = Random.Range(minTimeDiff, maxTimeDiff);
    }

    public void IncreaseSpeed()
    {
        minNumPoopsToSpawn += 1;
        maxNumPoopsToSpawn += 1;

        birdpoopSpeed += 2.0f;

        minTimeDiff -= 0.2f;
        maxTimeDiff -= 0.2f;
        if (minTimeDiff < 0.2f)
        {
            minTimeDiff = 0.2f;
        }
        if (maxTimeDiff < 0.2f)
        {
            maxTimeDiff = 0.2f;
        }

        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Obstacle"); //Find all GroundTag
        foreach (var groundObject in groundObjects)
        {
            groundObject.GetComponent<birdpoop>().SetSpeed(birdpoopSpeed);
        }

        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //Find all GroundTag
        foreach (var jellyObject in jellyObjects)
        {
            jellyObject.GetComponent<jelly>().SetSpeed(birdpoopSpeed);
        }

    }

    public void DecreaseSpeed()
    {
        minNumPoopsToSpawn -= 1;
        maxNumPoopsToSpawn -= 1;

        birdpoopSpeed -= 2.0f;

        minTimeDiff += 0.2f;
        maxTimeDiff += 0.2f;
        if (minTimeDiff < 0.2f)
        {
            minTimeDiff = 0.2f;
        }
        if (maxTimeDiff < 0.2f)
        {
            maxTimeDiff = 0.2f;
        }

        GameObject[] groundObjects = GameObject.FindGameObjectsWithTag("Obstacle"); //Find all GroundTag
        foreach (var groundObject in groundObjects)
        {
            groundObject.GetComponent<birdpoop>().SetSpeed(birdpoopSpeed);
        }

        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //Find all GroundTag
        foreach (var jellyObject in jellyObjects)
        {
            jellyObject.GetComponent<jelly>().SetSpeed(birdpoopSpeed);
        }




    }



}
