using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg11jelly : MonoBehaviour
{
    // Start is called before the first frame update
    public float jellySpeed = 5f; // 움직임 속도

    void Start()
    {

    }

    private void Update()
    {
        Vector2 direction = -transform.position.normalized; // (0, 0)으로 향하는 방향

        // 움직임 적용
        transform.Translate(direction * jellySpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Mg11manager.instance.AddScore();
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "egg")
        {
            
            Destroy(gameObject);
        }
    }




    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
