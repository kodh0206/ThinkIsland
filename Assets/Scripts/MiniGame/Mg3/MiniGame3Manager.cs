using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MiniGame3Manager : MonoBehaviour
{
    public static MiniGame3Manager instance = null;
    
    private int score = 0;
    public bool isStunned = false; // 물체가 현재 스턴 상태인지를 나타내는 부울 값입니다.

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore() 
    {
        score += 1;
        MiniGameManager.Instance.AddJelly();//젤리추가 
        if (score % 5 == 0)
        {
            PoopSpawner spawner = FindObjectOfType <PoopSpawner> ();
            if (spawner != null) 
            {
                spawner.DecreasePoopInterval();  // decrease interval
            }
        }
    }
    
    public void StunPlayer()
    {
        isStunned = true;
        StartCoroutine(RecoverFromStun());
    }

    private IEnumerator RecoverFromStun()
    {
        yield return new WaitForSeconds(2f);
        isStunned = false;
    }
    
}