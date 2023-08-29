using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mg16Fish1 : MonoBehaviour
{

    Mg16Player mg16Player;

    public float startY = -3.5f;
    public float topY = 2.5f;
    // 복어 끝 위치
    public float stopY = 1.7f;
    public float moveDuration = 1.5f;

    public bool playerIsTrigger = false;
    

    private void Start()
    {
        mg16Player = GameObject.FindObjectOfType<Mg16Player>();
        StartMovement();
    }

    private void Update()
    {
        if (playerIsTrigger)
        {
            Time.timeScale = 1;
            StartCoroutine(InvokeStunAndResetTrigger());
        }
    }

    private void StartMovement()
    {
        if (gameObject != null)
        {
            transform.DOMoveY(topY, moveDuration).SetEase(Ease.Linear).OnComplete(ReverseMovement);
        }
    }

    private void ReverseMovement()
    {
        if (gameObject != null)
        {
            transform.DOMoveY(stopY, moveDuration).SetEase(Ease.Linear);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mg16Player.GetHit();
            playerIsTrigger = true;

            Mg16Manager.instance.achievementFail = true;
        }
    }

    private IEnumerator InvokeStunAndResetTrigger()
    {
        yield return new WaitForSecondsRealtime(1f);
        StunAndResetTrigger();
        Time.timeScale = 1;
        mg16Player.PlayerColorChangeBack();
    }

    private void StunAndResetTrigger()
    {
        Time.timeScale = 1;
        playerIsTrigger = false;
    }

    public void IncreaseSpeed()
    {
        if (moveDuration >= 0.8f)
        {
            moveDuration -= 0.3f;
        }
    }

    public void DecreaseSpeed()
    {
        if (moveDuration <= 1.5f)
        {
            moveDuration += 0.3f;
        }
    }
}