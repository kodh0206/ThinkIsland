using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg20NomalBlock : MonoBehaviour
{
    public float blockspeed = 3f; // ������Ʈ�� �ӵ�
    
    void Start()
    {
        
    }

    

    private void Update()
    {
        // ������Ʈ�� ���� �̵���Ŵ
        transform.Translate(Vector3.up * blockspeed * Time.deltaTime);
    }


    public void SetSpeed(float speed)
    {
        blockspeed = speed;
    }
}
