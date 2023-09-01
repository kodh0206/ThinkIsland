using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg13jelly : MonoBehaviour
{

    [SerializeField]
    private NumberParticle numberParticlePrefab;

    public float jellySpeed = 5f; 
    private Transform target; 
    private bool getTarget=false;
    public Vector2 direction;

    private void Start()
    {
        
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
            
            transform.Translate(direction * jellySpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vibration.Instance.Vibrate();
            MiniGameManager.Instance.AddJelly();
            Mg13manager.instance.AddScore();

            int newScore =MiniGameManager.Instance.totalJelly;

            // 파티클 시스템 인스턴스 생성
            NumberParticle numberParticleInstance = Instantiate(numberParticlePrefab, transform.position, Quaternion.identity);
            numberParticleInstance.DisplayNumber(newScore, transform.position);
            
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
