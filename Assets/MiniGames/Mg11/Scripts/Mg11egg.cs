using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg11egg : MonoBehaviour
{

    public static Mg11egg instance = null;

    private Animator animator;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void EggBreak()
    {
        animator.SetBool("Break", true);

        GameObject[] ObstacleObjects = GameObject.FindGameObjectsWithTag("Obstacle"); //�ʵ� �ı�
        foreach (var ObstacleObject in ObstacleObjects)
        {
            Destroy(ObstacleObject);
        }
        GameObject[] jellyObjects = GameObject.FindGameObjectsWithTag("jelly"); //�ʵ� ���� �ı�
        foreach (var jellyObject in jellyObjects)
        {
            Destroy(jellyObject);
        }
    }

    public void EggBreakEnd()
    {
        animator.SetBool("Break", false);
    }


}
