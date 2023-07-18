using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1Jelly : MonoBehaviour
{
    [SerializeField]
    private float jellySpeed = 5.0f;

    void Update()
    {
        transform.position += Vector3.left * jellySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            MiniGameManager.Instance.AddJelly();
            Mg1Manager.instance.AddScore();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
