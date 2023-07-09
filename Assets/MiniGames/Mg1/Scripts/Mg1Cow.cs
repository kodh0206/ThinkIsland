using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mg1Cow : MonoBehaviour
{
    public Button rightButton;
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
        if (!mg1Player.isTrigger)
        {
            PolygonCollider2D cowCollider = gameObject.GetComponent<PolygonCollider2D>();
            if (cowCollider != null)
            {
                cowCollider.isTrigger = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && this.gameObject.tag =="cow")
        {   
            //AudioManager.Instance.PlayCow();
        }
    }

    public void SetSpeed(float speed)
    {
        cowSpeed = speed;
    }
}
