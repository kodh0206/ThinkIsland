using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Mg17shell : MonoBehaviour
{
    public float obstacleSpeed = 5.0f;

    public GameObject jellyinShell;
    public GameObject ObstacleinRock;
    public Camera myCamera;

    public Sprite[] sprites = new Sprite[3];
    public Sprite sprite;

    public int HItCount = 0;

    void Update()
    {
        transform.position += Vector3.down * obstacleSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.gameObject.CompareTag("Player"))
        {   
            AudioManager.Instance.Rock();
            other.gameObject.GetComponent<Mg17Player>().GetHit();

            Mg17manager.instance.GameLevelDown(); //levelDown

            Mg17Spawner spawner1 = FindAnyObjectByType<Mg17Spawner>();
            spawner1.GetHit();
            Mg17RockSpawner spawner2 = FindAnyObjectByType<Mg17RockSpawner>();
            spawner2.GetHit();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Rock"))
        {   
            AudioManager.Instance.CapsuleBreak();
            Hit();
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[HItCount];
            HItCount += 1;

            ShakeCamera();

            if (HItCount == 1)
            {
                float randomValue = Random.value;
                if (randomValue < 0.7f)
                {
                    Vector3 shellPosition = transform.position;
                    Instantiate(jellyinShell, shellPosition, Quaternion.identity);
                }
                else
                {
                    Vector3 shellPosition = transform.position;
                    Instantiate(ObstacleinRock, shellPosition, Quaternion.identity);
                }
                Destroy(gameObject);
            }

            
        }
    }


    public IEnumerator Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        spriteRenderer.color = new Color(0f, 0f, 0f); 

        yield return new WaitForSeconds(0.5f); 

        spriteRenderer.color = originalColor; 
    }

    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }

    public void ShakeCamera()
    {
        myCamera.transform.DOShakePosition(0.1f, 0.2f, 2);  
    }
}
