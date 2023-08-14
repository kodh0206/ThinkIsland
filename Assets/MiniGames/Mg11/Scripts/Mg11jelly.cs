using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg11jelly : MonoBehaviour
{

    [SerializeField]
    private NumberParticle numberParticlePrefab;

    // Start is called before the first frame update
    public float jellySpeed = 5f; 

    void Start()
    {

    }

    private void Update()
    {
        Vector2 direction = -transform.position.normalized; 

        
        transform.Translate(direction * jellySpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            MiniGameManager.Instance.AddJelly();
            Mg11manager.instance.AddScore();

            int newScore =MiniGameManager.Instance.totalJelly;

            // 파티클 시스템 인스턴스 생성
            NumberParticle numberParticleInstance = Instantiate(numberParticlePrefab, transform.position, Quaternion.identity);
            numberParticleInstance.DisplayNumber(newScore, transform.position);
            
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
