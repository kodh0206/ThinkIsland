using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class birdpoop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float birdpoop_Speed = 5.0f;

    [SerializeField]
    public float birdpoopupdownSpeed = 10.0f;

    [SerializeField]
    private float pushForce = 5.0f; // �б� ��

    int count = 0;
    bool updown = true;

    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Player ������Ʈ�� ã���ϴ�.
            GameObject player = other.gameObject;

            // Player�� �輳�� ������ �б� ������ ����մϴ�.
            Vector2 pushDirection = (player.transform.position - transform.position).normalized;

            // Player ������Ʈ�� Rigidbody2D ������Ʈ�� ���� ���, �б� ���� �����մϴ�.
            Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left* birdpoop_Speed * Time.deltaTime;

        if (updown == true)
        {
            transform.position += Vector3.down * birdpoopupdownSpeed * Time.deltaTime;
            if (count < 50)
            {
                count++;
            }
            else
            {
                count = 0;
                updown = false;
            }
        }
        else if (updown == false)
        {
            transform.position += Vector3.up * birdpoopupdownSpeed * Time.deltaTime;
            if (count < 50)
            {
                count++;
            }
            else
            {
                count = 0;
                updown = true;
            }
        }

    }
    public void SetSpeed(float speed)
    {
        birdpoop_Speed = speed;
    }
}
