using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg13jelly : MonoBehaviour
{
    public float jellySpeed = 5f; // 움직임 속도
    private Transform target; // 목표 오브젝트의 Transform
    private bool getTarget=false;
    public Vector2 direction;

    private void Start()
    {
        // Player 태그를 가진 오브젝트를 찾아 목표로 설정
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if ((player != null) && !(getTarget))
        {
            target = player.transform;
            getTarget=true;
            direction = (target.position - transform.position).normalized;

        }
    }

    private void Update()
    {
        if (target != null)
        {
            // 움직임 적용
            transform.Translate(direction * jellySpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Mg13manager.instance.AddScore();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
