using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg3jelly : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private NumberParticle numberParticlePrefab;
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" ) //poop2 is jelly
        {
            MiniGameManager.Instance.AddJelly();
            int newScore =MiniGameManager.Instance.totalJelly;
            // 파티클 시스템 인스턴스 생성
            NumberParticle numberParticleInstance = Instantiate(numberParticlePrefab, transform.position, Quaternion.identity);
            numberParticleInstance.DisplayNumber(newScore, transform.position);
            Destroy(this.gameObject);

        }
        else if (other.gameObject.tag == "poop" || other.gameObject.tag == "jelly")
        {
            return;
        }

        Instantiate(particle, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().Play();
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(gameObject, 1f);
    }
}
