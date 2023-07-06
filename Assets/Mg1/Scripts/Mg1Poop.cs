using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1Poop : MonoBehaviour
{
    [SerializeField]
    private float poopSpeed = 5.0f;

    private Mg1Player mg1Player;

    private void Start()
    {
        mg1Player = GameObject.FindObjectOfType<Mg1Player>();
    }

    void Update()
    {
        transform.position += Vector3.left * poopSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("poop"))
        {   
            AudioManager.Instance.PlayPoop();
            mg1Player.StunPlayer();
            other.gameObject.GetComponent<Mg1Player>().GetObstacle();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        poopSpeed = speed;
    }
}
