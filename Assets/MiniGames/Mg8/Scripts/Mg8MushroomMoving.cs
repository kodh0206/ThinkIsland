using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg8MushroomMoving : MonoBehaviour
{

    Animator animator;

    public static Mg8MushroomMoving instance = null;

    public int MUshroomCount=0;


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
        MUshroomCount=Mg8Player.instance.MushroomCount();
        animator.SetInteger("Mushroomnumber",MUshroomCount);
    }

    public void setCount(int Count)
    {
        MUshroomCount = Count;
    }

}
