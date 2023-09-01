using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg6jelly : MonoBehaviour
{
    [SerializeField]
    public float jellySpeed = 5.0f;

    [SerializeField]
    private NumberParticle numberParticlePrefab;

    public bool jellydirection = true;

    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (jellydirection)
        {
            transform.position += Vector3.right * jellySpeed * Time.deltaTime;
            
        }
        else
        {
            transform.position += Vector3.left * jellySpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vibration.Instance.Vibrate();
            MiniGameManager.Instance.AddJelly();
            Mg6manager.instance.AddScore();

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
