using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentAndTeacherMove : MonoBehaviour
{

    public Animator animator;
    public float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    public void GetAnswer()
    {
        animator.SetBool("Answer", true);
        StartCoroutine(ResetAnswerAfterDelay());
    }

    private IEnumerator ResetAnswerAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("Answer", false);
    }
}
