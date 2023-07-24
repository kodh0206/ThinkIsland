using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg6Obstacle : MonoBehaviour
{
    [SerializeField]
    public float obstacleSpeed = 5.0f;
    // Start is called before the first frame update

    public bool obstacledirection = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (obstacledirection)
        {
            transform.position += Vector3.right * obstacleSpeed * Time.deltaTime;

        }
        else
        {
            transform.position += Vector3.left * obstacleSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {   
            //AudioManager.Instance.Spider();
            other.gameObject.GetComponent<Mg6Player>().GetHit();
        }
    }
    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}
