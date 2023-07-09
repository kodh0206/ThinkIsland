using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg7Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float jumpPower = 5.0f;
    public int level;

    private  AudioSource audioSource;
    public AudioClip jump;

    private bool RightButton = false;
    private bool LeftButton = false;

    public void RightClick()
    {
        LeftButton = false;
        RightButton = true;

    }

    public void RightClickOff()
    {
        RightButton = false;

    }

    public void LeftClick()
    {
        RightButton = false;
        LeftButton = true;
    }

    public void LeftClickOff()
    {
        LeftButton = false;
    }

    public Mg7Player()
    {
        level = 1;
    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // If AudioSource is not attached to the gameObject
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || LeftButton)
        {
            JumpWithAngle(135f);
            LeftButton = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)||RightButton)
        {
            JumpWithAngle(45f);
            RightButton = false;
        }
    }

    void JumpWithAngle(float angle)
    {   
        audioSource.PlayOneShot(jump);
        float radian = angle * Mathf.Deg2Rad;
        Vector2 jumpDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        rigidbody2D.velocity = jumpDirection * jumpPower;
    }
}
