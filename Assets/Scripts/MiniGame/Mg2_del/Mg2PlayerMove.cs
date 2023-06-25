using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg2PlayerMove : MonoBehaviour
{

    private void Start()
    {
    }

    public void Button1Pressed()
    {
        if (transform.position.x == 0)
        {
            transform.position = new Vector2(-4.3f, 0f);
        }
        else if (transform.position.x == -4.3f)
        {
            transform.position = new Vector2(4.3f, 0f);
        }
        else if (transform.position.x == 4.3f)
        {
            transform.position = Vector2.zero;
        }
    }

    public void Button2Pressed()
    {
        if (transform.position.x == 0)
        {
            transform.position = new Vector2(4.3f, 0f);
        }
        else if (transform.position.x == -4.3f)
        {
            transform.position = Vector2.zero;
        }
        else if (transform.position.x == 4.3f)
        {
            transform.position = new Vector2(-4.3f, 0f);
        }
    }
}
