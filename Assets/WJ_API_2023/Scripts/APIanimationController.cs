using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIanimationController : MonoBehaviour
{

    public static APIanimationController instance = null;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAnimation()
    {
        StudentAndTeacherMove[] objects = GameObject.FindObjectsOfType(typeof(StudentAndTeacherMove)) as StudentAndTeacherMove[];

        foreach (StudentAndTeacherMove obj in objects)
        {
            // obj에 대한 작업 수행
            obj.GetAnswer();
        }

    }
}
