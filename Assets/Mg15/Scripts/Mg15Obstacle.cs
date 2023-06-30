using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg15Obstacle : MonoBehaviour
{
    public float gravityScaleIncreaseRate = 1f; // gravity scale이 상승하는 비율
    public float maxGravityScale = 1f; // 최대 gravity scale 값

    private Rigidbody2D rb;
    private bool isScaleChanged = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = -1f; // 시작할 때 gravity scale을 -1로 설정
    }

    private void Update()
    {
        // gravity scale을 상승시킴
        //rb.gravityScale += gravityScaleIncreaseRate * Time.deltaTime;

        // 최대 gravity scale 값을 초과하지 않도록 제한
        //rb.gravityScale = Mathf.Clamp(rb.gravityScale, -1f, maxGravityScale);

        // gravity scale이 0이 되면 스케일 변경
        //if (rb.gravityScale >= 0f && !isScaleChanged)
       // {
            //transform.localScale = Vector3.one; // 스케일을 (1, 1, 1)로 변경
            //isScaleChanged = true; // 스케일 변경 상태를 표시
       // }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("stair"))
        {
            rb.gravityScale = 1.0f;
            transform.localScale = Vector3.one; // 스케일을 (1, 1, 1)로 변경
            isScaleChanged = true; // 스케일 변경 상태를 표시
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Mg15Player>().GetHit();
        }
    }

    public void SetSpeed(float speed)
    {
        gravityScaleIncreaseRate = speed;
    }
}
