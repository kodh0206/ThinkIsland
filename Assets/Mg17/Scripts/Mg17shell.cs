using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg17shell : MonoBehaviour
{
    public float obstacleSpeed = 5.0f;

    public GameObject jellyinShell;
    public GameObject ObstacleinRock;

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
            other.gameObject.GetComponent<Mg17Player>().GetHit();

            Mg17Spawner spawner1 = FindAnyObjectByType<Mg17Spawner>();
            spawner1.GetHit();
            Mg17RockSpawner spawner2 = FindAnyObjectByType<Mg17RockSpawner>();
            spawner2.GetHit();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Rock"))
        {
            Hit();
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[HItCount];
            HItCount += 1;
            

            if (HItCount == 2)
            {
                float randomValue = Random.value;
                if (randomValue < 0.5f)
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

        spriteRenderer.color = new Color(0f, 0f, 0f); // 변경할 색

        yield return new WaitForSeconds(0.5f); // 변경된 색을 유지할 시간

        spriteRenderer.color = originalColor; // 원래 색으로 되돌리기
    }

    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}
