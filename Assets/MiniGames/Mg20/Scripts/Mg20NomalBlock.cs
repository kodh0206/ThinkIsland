using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20NomalBlock : MonoBehaviour
{
    public float blockspeed = 3f; // 오브젝트의 속도
    
    void Start()
    {
        
    }

    

    private void Update()
    {
        // 오브젝트를 위로 이동시킴
        transform.Translate(Vector3.up * blockspeed * Time.deltaTime);
    }


    public void SetSpeed(float speed)
    {
        blockspeed = speed;
    }
}
