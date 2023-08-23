using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Mg4Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float jumpPower = 5.0f;
    public float moveSpeed = 5.0f;
    public float targetXPosition = -5.4f; //�з����� �� ���ƿ� ��

    private AudioSource audioSource;
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

    
    public int level;
    public Mg4Player()
    {
        level = 1;
    }

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || RightButton) 
        {   
            if(AudioManager.Instance.isSFXOn){
            audioSource.PlayOneShot(jump);
            }
            else{
                Debug.Log("SFX off");
            }
            rigidbody2D.velocity = Vector2.up * jumpPower;
            RightButton = false;
        }

        if (transform.position.x != targetXPosition)
        {
            
            Vector2 targetPosition = new Vector2(targetXPosition, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime / 4f);
        }

    }
}
