using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg8jelly : MonoBehaviour
{
    [SerializeField]
    private float jellySpeed = 5.0f;

    [SerializeField]
    private NumberParticle numberParticlePrefab;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * jellySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            MiniGameManager.Instance.AddJelly();
            Mg8manager.instance.AddScore();

            int newScore =MiniGameManager.Instance.totalJelly;

            // 파티클 시스템 인스턴스 생성
            NumberParticle numberParticleInstance = Instantiate(numberParticlePrefab, transform.position, Quaternion.identity);
            numberParticleInstance.DisplayNumber(newScore, transform.position);

            Destroy(gameObject);
            
        }

        if (other.gameObject.tag == "Obstacle")
        {
            
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
