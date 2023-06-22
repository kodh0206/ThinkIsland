using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private float moveSpeed = 5f;
    // Start is called before the first frame update

    public int level;

    public Player()
    {
        level = 1; ///초기화시 다른 곳에서 가져올 수 있으면 될듯.
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
{
    if (GameManager.instance.isGameOver == false)
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Vector3 currScale = transform.localScale;
            transform.localScale = new Vector3(-Mathf.Abs(currScale.x),currScale.y,currScale.z);
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            animator.SetBool("isRunning", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Vector3 currScale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(currScale.x),currScale.y,currScale.z);        
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            animator.SetBool("isRunning", true);
        }else
        {
        animator.SetBool("isRunning", false);
        }
        
    }else
    {
        animator.SetBool("isRunning", false);
    }
}
    public void GetPoop()
    {
        StartCoroutine(DisableControlAndResetColor());
    }

    private IEnumerator DisableControlAndResetColor()
    {
        // 조작 비활성화
        enabled = false;

        // 색상 변경
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
        }

        // 2초간 대기
        yield return new WaitForSeconds(2f);

        // 조작 활성화
        enabled = true;

        // 1초간 poop 영향 받지 않음
        yield return new WaitForSeconds(1f);

        // 색상 원래대로 복구
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
