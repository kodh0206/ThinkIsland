using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg12shell : MonoBehaviour
{
    public float obstacleSpeed = 5.0f;

    public GameObject jellyinShell;

    void Update()
    {
        transform.position += Vector3.left * obstacleSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Mg12Player>().GetHit();
        }

        if (other.gameObject.CompareTag("Rock"))
        {
            // ������ Ȯ���� jellyinShell ����
            float randomValue = Random.value;
            if (randomValue < 0.5f)
            {
                Vector3 shellPosition = transform.position;
                Instantiate(jellyinShell, shellPosition, Quaternion.identity);
            }
            AudioManager.Instance.ShellBreak();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}
