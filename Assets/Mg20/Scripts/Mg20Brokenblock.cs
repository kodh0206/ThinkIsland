using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20Brokenblock : MonoBehaviour
{
    public GameObject breakEffect; // 부서질 때 재생될 파티클 효과

    public float blockspeed = 3f; // 오브젝트의 속도

    private void Update()
    {
        // 오브젝트를 위로 이동시킴
        transform.Translate(Vector3.up * blockspeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BreakObject();
        }
    }

    private void BreakObject()
    {
        // 파티클 효과 재생
        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }

        // 0.5초 지연 후 오브젝트 제거
        Invoke("DestroyObject", 0.5f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void SetSpeed(float speed)
    {
        blockspeed = speed;
    }
}
