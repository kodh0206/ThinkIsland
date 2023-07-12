using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg5jellyspawner : MonoBehaviour
{
    public GameObject Mg5jelly;
    [SerializeField]
    public int minNumMg5jellyToSpawn = 3; // 최소 생성 젤리 개수
    [SerializeField]
    public int maxNumMgMg5jellyToSpawn = 4; // 최대 생성 젤리 개수

    [SerializeField]
    private float Mg5jellySpeed = 4.0f; // 장애물의 초기 속도

    [SerializeField]
    public float minTimeDiff = 2.0f; // 최소 생성 간격
    [SerializeField]
    public float maxTimeDiff = 3.0f; // 최대 생성 간격


    float time = 0;
    float timeDiff = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomTimeDiff();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeDiff)
        {
            int numMg5jellyToSpawn = Random.Range(minNumMg5jellyToSpawn, maxNumMgMg5jellyToSpawn + 1);

            for (int i = 0; i < numMg5jellyToSpawn; i++)
            {
                GameObject new_Mg5jelly = Instantiate(Mg5jelly);
                float posX = Random.Range(-3f, 7.85f); // x 좌표를 랜덤으로 설정
                new_Mg5jelly.transform.position = new Vector3(posX, -5, 0);
                new_Mg5jelly.GetComponent<Mg5jelly>().SetSpeed(Mg5jellySpeed);

                Destroy(new_Mg5jelly, 4f);
            }

            time = 0;
            SetRandomTimeDiff(); // 다음 생성 간격을 랜덤으로 설정
        }
    }

    void SetRandomTimeDiff()
    {
        timeDiff = Random.Range(minTimeDiff, maxTimeDiff);
    }

    public void IncreaseSpeed()
    {


        Mg5jellySpeed += 1.0f;

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
    }


    public void DecreaseSpeed()
    {


        Mg5jellySpeed -= 1.0f;

        minTimeDiff += 0.2f;
        maxTimeDiff += 0.2f;
        
    }

}
