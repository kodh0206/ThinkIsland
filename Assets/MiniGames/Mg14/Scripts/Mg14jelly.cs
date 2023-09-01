using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg14jelly : MonoBehaviour
{
    [SerializeField]
    private NumberParticle numberParticlePrefab;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vibration.Instance.Vibrate();
            MiniGameManager.Instance.AddJelly();
            Mg14manager.instance.AddScore();

            int newScore =MiniGameManager.Instance.totalJelly;

            // 파티클 시스템 인스턴스 생성
            NumberParticle numberParticleInstance = Instantiate(numberParticlePrefab, transform.position, Quaternion.identity);
            numberParticleInstance.DisplayNumber(newScore, transform.position);
            
            Destroy(gameObject);
        }
    }
}
