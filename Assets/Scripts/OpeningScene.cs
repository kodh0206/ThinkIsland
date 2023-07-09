using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<SpriteRenderer> Scene1;
    private bool scene1IsPlayed=false;
    void Scene1Sequence()
    {       
         if (Input.touchCount > 0)
        {
            // 첫 번째 터치 이벤트를 가져옴
            Touch touch = Input.GetTouch(0);

            // 터치가 시작된 경우
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch detected");
                StartFadeIn(Scene1[1]);
            }

            scene1IsPlayed=true;
        }
    }

    void Update()
    {
        if(!scene1IsPlayed)
        {   Debug.Log("Play");
            Scene1Sequence();
        }
    }
    public float fadeDuration = 1.0f;

        public void StartFadeOut(SpriteRenderer sprite)
    {
        StartCoroutine(FadeOut(sprite));
    }

    public void StartFadeIn(SpriteRenderer sprite)
    {
        StartCoroutine(FadeIn(sprite));
    }

 IEnumerator FadeOut(SpriteRenderer sprite)
    {
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            sprite.color = Color.Lerp(sprite.color, new Color(1, 1, 1, 0), Mathf.Min(1, t/fadeDuration));
            yield return null;
        }
    }

   IEnumerator FadeIn(SpriteRenderer sprite)
    {
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            sprite.color = Color.Lerp(sprite.color, new Color(1, 1, 1, 1), Mathf.Min(1, t/fadeDuration));
            yield return null;
        }
    }
}
