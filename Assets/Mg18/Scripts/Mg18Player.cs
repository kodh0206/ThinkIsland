using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg18Player : MonoBehaviour
{
    public float maxGravityScale = 1f; // �ִ� gravity scale ��

    public float moveSpeed = 5f; // ������ �ӵ�

    private float jumpForce = 12.0f; // �ʱ� ���� ��

    public float targetXPosition = -7.5f; //�з����� �� ���ƿ� ��

    private bool isHit = false;

    private BoxCollider2D boxCollider;

    [SerializeField]
    private bool nowJumping = false;

    private Rigidbody2D rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // ������ �� gravity scale�� 0�� ����
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            rb.gravityScale = Mathf.Clamp(rb.gravityScale, 0f, maxGravityScale);
        }

        // y ��ǥ�� ������ ��� gravity scale ����
        if (!(isHit) && transform.position.y <= 0 )
        {
            float gravityScale = transform.position.y; // y ��ǥ�� ������ gravity scale�� ���
            gravityScale = Mathf.Clamp(gravityScale, -maxGravityScale, 0f ); // �ִ밪 ����
            rb.gravityScale = gravityScale/ Random.Range(1f,3f);
 
        }
        else
        {
            rb.gravityScale = 1.0f;
        }

        if (!(isHit) &&  Input.GetKey(KeyCode.LeftArrow) && !nowJumping) // ���� ��Ұų� �� �� �϶�.
        {
            Jump();
            nowJumping = true;
            
        }

        if (!(isHit) && transform.position.x != targetXPosition)
        {
            // ���� ��ġ�� ��ǥ ��ġ�� ���Ͽ� X ��ġ �̵� ó��
            Vector2 targetPosition = new Vector2(targetXPosition, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime/4f);
        }

    }


    private void Jump()
    {
        rb.gravityScale = 0.5f;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground" )
        {
            nowJumping = false;
        }
        if (other.gameObject.tag == "water")
        {
            nowJumping = false;
        }

    }

    public void GetHit()
    {
        // ������ ����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        isHit= true;

        // �񵿱� ó�� ����
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        enabled = false;
        rb.gravityScale = 2f;
        boxCollider.enabled = true;

        yield return new WaitForSeconds(2f);

        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }



}
