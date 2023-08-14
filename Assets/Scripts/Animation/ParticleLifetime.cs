using UnityEngine;
using System.Collections;
using AssetKits.ParticleImage;


public class ParticleLifetime : MonoBehaviour
{
    public ParticleImage particleImg;
    public float duration = 5.0f; // 원하는 시간을 여기에 설정

    private void Start()
    {
        if (particleImg == null)
            particleImg = GetComponent<ParticleImage>();

        StartCoroutine(PlayForDuration(duration));
    }

    private IEnumerator PlayForDuration(float time)
    {
        particleImg.Play();
        yield return new WaitForSeconds(time);
        particleImg.Stop();
    }
}
