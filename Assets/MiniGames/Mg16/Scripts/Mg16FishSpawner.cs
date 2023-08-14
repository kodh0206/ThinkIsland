using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16FishSpawner : MonoBehaviour
{
    Mg16Fish1 mg16Fish1;
    Mg16Fish2 mg16Fish2;
    Mg16Battery mg16Battery;

    public GameObject fish1;
    public GameObject fish2;
    public Transform player;
    public Animator animator;  // 애니메이터 컴포넌트

    [SerializeField]
    // fish & jelly가 올라왔다 내려옴(3초 소요) + 정지 (1초 소요)
    public float time_diff = 4f;
    float time = 0;

    // x값 랜덤
    private float randomX1;
    private float randomX2;

    void Start()
    {
        mg16Fish1 = GetComponent<Mg16Fish1>();
        mg16Fish2 = GetComponent<Mg16Fish2>();
        mg16Battery = FindObjectOfType<Mg16Battery>();

        animator = GetComponent<Animator>();  // 애니메이터 컴포넌트 가져오기

        randomX1 = Random.Range(-9.5f, 9.5f);
        randomX2 = Random.Range(-9.5f, 9.5f);

        if (randomX1 == randomX2)
        {
            randomX2 = Random.Range(-9.5f, 9.5f);
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > time_diff)
        {   if(AudioManager.Instance.isSFXOn)
                {
                    mg16Battery.audioSource.PlayOneShot(mg16Battery.electricity);
                }
            // 전기 활성화 함수
            mg16Battery.ElectricitySetActiveTrue();


            GameObject new_fish1 = Instantiate(fish1);
            GameObject new_fish2 = Instantiate(fish2);

            Vector2 spawnPosition1 = new Vector2(randomX1, -3.5f);
            Vector2 spawnPosition2 = new Vector2(randomX2, -3.5f);

            new_fish1.transform.position = spawnPosition1;
            new_fish2.transform.position = spawnPosition2;

            new_fish1.GetComponent<Mg16Fish1>();
            new_fish2.GetComponent<Mg16Fish2>();

            randomX1 = Random.Range(-9.5f, 9.5f);
            randomX2 = Random.Range(-9.5f, 9.5f);

            if (randomX1 == randomX2)
            {
                randomX2 = Random.Range(-9.5f, 9.5f);
            }

            time = 0;

            if (gameObject != null)
            {
                Destroy(new_fish1, time_diff);
                Destroy(new_fish2, time_diff);

                // time_diff - 1초 후에 BatterySetBoolTrue() 함수를 호출합니다.
                Invoke("BatterySetBoolTrue", time_diff - 1f);
            }
        }
    }

    private void BatterySetBoolTrue()
    {
        mg16Battery.BatterySetBoolTrue();
        Invoke("BatterySetBoolFalse", 1f);
    }

    private void BatterySetBoolFalse()
    {
        mg16Battery.BatterySetBoolFalse();
    }

    public void IncreaseSpeed()
    {
        if (time_diff >= 3.1f)
        {
            time_diff -= 0.3f;
        }
    }

    public void DecreaseSpeed()
    {
        if (time_diff <= 4.0f)
        {
            time_diff += 0.3f;
        }
    }
}
