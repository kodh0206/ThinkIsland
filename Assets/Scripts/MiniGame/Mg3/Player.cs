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
        level = 1; ///�ʱ�ȭ�� �ٸ� ������ ������ �� ������ �ɵ�.
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
{
    if (MiniGame3Manager.instance.isGameOver == false)
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
        // ���� ��Ȱ��ȭ
        enabled = false;

        // ���� ����
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.77f, 0.52f, 0f);
        }

        // 2�ʰ� ���
        yield return new WaitForSeconds(2f);

        // ���� Ȱ��ȭ
        enabled = true;

        // 1�ʰ� poop ���� ���� ����
        yield return new WaitForSeconds(1f);

        // ���� ������� ����
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
