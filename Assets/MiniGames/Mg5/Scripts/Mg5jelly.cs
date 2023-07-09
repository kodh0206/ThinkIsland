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
        moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), 0f).normalized;
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
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        jellySpeed = speed;
    }
}
