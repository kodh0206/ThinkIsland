using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg12shell : MonoBehaviour
{

    public Camera myCamera;
    Animator anim;
    public float obstacleSpeed = 5.0f;

    public GameObject jellyinShell;

    public int HItCount = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position += Vector3.left * obstacleSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Mg12manager.instance.GameLevelDown();
            other.gameObject.GetComponent<Mg12Player>().GetHit();
        }

        if (other.gameObject.CompareTag("Rock"))
        {
            HItCount += 1;
            anim.SetBool("isTrigger", true);

            ShakeCamera();

            // 조개가 돌에 두 번 맞으면 부숴진다.
            if (HItCount == 2)
            {
                float randomValue = Random.value;
                if (randomValue < 0.5f)
                {
                    Vector3 shellPosition = transform.position;
                    Instantiate(jellyinShell, shellPosition, Quaternion.identity);
                }
                Destroy(gameObject);
            }
            AudioManager.Instance.ShellBreak();
        }
    }

    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }


    public void ShakeCamera()
    {
        myCamera.transform.DOShakePosition(0.6f, 0.6f, 4);  // 카메라를 0.2초 동안, 강도 0.2로 4번 흔듭니다.
    }

}
