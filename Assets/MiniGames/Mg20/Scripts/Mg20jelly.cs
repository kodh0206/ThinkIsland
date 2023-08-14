using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20jelly : MonoBehaviour
{

    [SerializeField]
    private NumberParticle numberParticlePrefab;

    public float jellySpeed = 2.0f;


    private void Start()
    {

    }

    private void Update()
    {
        transform.position += Vector3.up * jellySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {   
            MiniGameManager.Instance.AddJelly();
            Mg20manager.instance.AddScore();

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
