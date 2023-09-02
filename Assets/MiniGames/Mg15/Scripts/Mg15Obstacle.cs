using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg15Obstacle : MonoBehaviour
{
    public float gravityScaleIncreaseRate = 1f; 
    public float maxGravityScale = 1f; 

    private Rigidbody2D rb;
    private bool isScaleChanged = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = -1f; 
    
    }

    private void Update()
    {
        // gravity scale�� ��½�Ŵ
        //rb.gravityScale += gravityScaleIncreaseRate * Time.deltaTime;

        // �ִ� gravity scale ���� �ʰ����� �ʵ��� ����
        //rb.gravityScale = Mathf.Clamp(rb.gravityScale, -1f, maxGravityScale);

        // gravity scale�� 0�� �Ǹ� ������ ����
        //if (rb.gravityScale >= 0f && !isScaleChanged)
       // {
            //transform.localScale = Vector3.one; // �������� (1, 1, 1)�� ����
            //isScaleChanged = true; // ������ ���� ���¸� ǥ��
       // }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("jelly"))
        {
            Destroy(gameObject);
        }

            if (other.gameObject.CompareTag("stair"))
        {
            rb.gravityScale = 1.0f;
            transform.localScale = new Vector3(1, -1, 1); 
            isScaleChanged = true; 
        }

        if (other.gameObject.CompareTag("Player"))
        {   
            AudioManager.Instance.ObstacleFly();
            other.gameObject.GetComponent<Mg15Player>().GetHit();

            Mg15manager.instance.achievementFail = true;
        }
    }

    public void SetSpeed(float speed)
    {
        gravityScaleIncreaseRate = speed;
    }
}
