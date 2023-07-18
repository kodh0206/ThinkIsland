using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg5jelly : MonoBehaviour
{
    [SerializeField]
    private float jellySpeed = 5.0f;

    private Vector3 moveDirection;

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
            MiniGameManager.Instance.AddJelly();
            Mg5manager.instance.AddScore();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
