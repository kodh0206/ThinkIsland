using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg5jelly : MonoBehaviour
{
    [SerializeField]
    private float jellySpeed = 5.0f;

    private Vector3 moveDirection;
    [SerializeField]
    private NumberParticle numberParticlePrefab;
    void Start()
    {
        moveDirection = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0, 0.7f), 0f).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * jellySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vibration.Instance.Vibrate();
            MiniGameManager.Instance.AddJelly();
            Mg5manager.instance.AddScore();
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
