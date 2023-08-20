using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg9Obstacle : MonoBehaviour
{
    [SerializeField]
    public float obstacleSpeed = 5.0f;
    // Start is called before the first frame update


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Vector3.left * obstacleSpeed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {    
            AudioManager.Instance.PlayPoop();
            //Mg9manager.instance.GameLevelDown();
            other.gameObject.GetComponent<Mg9Player>().GetHit();
        }
    }
    public void SetSpeed(float speed)
    {
        obstacleSpeed = speed;
    }
}
