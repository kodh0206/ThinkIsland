using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg16Jelly : MonoBehaviour
{
    Mg16Manager mg16Manager;

    public float startY = -3.5f;
    public float topY = 2.5f;
    // 젤리 끝 위치
    public float stopY = 1.7f;
    public float moveDuration = 1.5f;
    

    private void Start()
    {
        mg16Manager = FindObjectOfType<Mg16Manager>();
        StartMovement();
    }

    private void StartMovement()
    {
        transform.DOMoveY(topY, moveDuration).SetEase(Ease.Linear).OnComplete(ReverseMovement);
    }

    private void ReverseMovement()
    {
        transform.DOMoveY(stopY, moveDuration).SetEase(Ease.Linear);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            MiniGameManager.Instance.AddJelly();
            //mg16Manager?.AddScore();
            gameObject.SetActive(false);
        }
    }

    public void SpeedTime()
    {
        if (moveDuration >= 0.8f)
        {
            moveDuration -= 0.3f;
        }
    }
}
