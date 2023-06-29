using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg7jelly : MonoBehaviour
{
    private float jellySpeed = 5.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * jellySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            MiniGameManager.Instance.AddJelly(); //�����Դ� �κ�
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
