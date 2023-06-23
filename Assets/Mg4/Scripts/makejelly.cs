using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makejelly : MonoBehaviour
{
    public GameObject jelly;

    [SerializeField]
    private float jellySpeed = 5.0f; // 젤리의 초기 스피드

    [SerializeField]
    private float time_diff = 1.5f;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > time_diff)
        {
            GameObject new_jelly = Instantiate(jelly);
            new_jelly.transform.position = new Vector3(5.5f, Random.Range(-1.6f, 3.7f), 0);
            new_jelly.GetComponent<jelly>().SetSpeed(jellySpeed); // 젤리의 스피드 설정
            time = 0;
            Destroy(new_jelly, 10.0f);
        }
    }

    public void IncreaseSpeed()
    {
        jellySpeed += 2.0f; // 젤리의 스피드 증가
    }
}
