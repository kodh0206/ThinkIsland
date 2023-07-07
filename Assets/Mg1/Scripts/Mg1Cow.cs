using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1Cow : MonoBehaviour
{
    [SerializeField]
    private float cowSpeed = 5.0f;
    private Mg1Player mg1Player;

    private void Start()
    {
        mg1Player = GameObject.FindObjectOfType<Mg1Player>();  
    }

    void Update()
    {
        transform.position += Vector3.left * cowSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && this.gameObject.tag =="cow")
        {   AudioManager.Instance.PlayCow();
            mg1Player.StunPlayer();
            other.gameObject.GetComponent<Mg1Player>().GetObstacle();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        cowSpeed = speed;
    }
}
