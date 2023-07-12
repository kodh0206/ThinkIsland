using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<SpriteRenderer> Scene1;
    [SerializeField]
    List<SpriteRenderer> Scene2;
    [SerializeField]
    List<SpriteRenderer> Scene3;
    [SerializeField]
    List<SpriteRenderer> Scene4;
    [SerializeField]
    List<SpriteRenderer> Scene5;
    [SerializeField]
    List<SpriteRenderer> Scene;



    [SerializeField]
    Camera cam;
    private bool scene1IsPlayed=false;
  
    

    public delegate void Sequence();

    private List<Sequence> sequences = new List<Sequence>();
    private int currentSequence = 0;

    private void Start()
    {
        // Add sequences
        sequences.Add(Sequence1);
        sequences.Add(Sequence2);
        sequences.Add(Sequence3);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("touch!!");
            if (touch.phase == TouchPhase.Began)
            {
                // Execute current sequence and move to the next
                if (currentSequence < sequences.Count)
                {
                    sequences[currentSequence].Invoke();
                    currentSequence++;
                }
                else
                {
                    Debug.Log("All sequences completed");
                }
            }
        }
    }

    private void Sequence1()
    {
         // 첫 번째 터치 이벤트를 가져
                Debug.Log("Touch detected");
                StartFadeIn(Scene1[0]);
            
            scene1IsPlayed=true;
    }

    private void Sequence2()
    {
        
        // Code for sequence 2 goes here
    }

    private void Sequence3()
    {
        Debug.Log("Running Sequence 3");
        // Code for sequence 3 goes here
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
