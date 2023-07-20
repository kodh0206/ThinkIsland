using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19ButtonController : MonoBehaviour
{
    public GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

    public static Mg19ButtonController instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void OnRightButtonClick()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Mg19Player playerScript = playerObject.GetComponent<Mg19Player>();
            if (playerScript != null)
            {
                playerScript.RightClick();
            }
        }
    }



    public void OnRightButtonClickoff()
    {
         playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Mg19Player playerScript = playerObject.GetComponent<Mg19Player>();
            if (playerScript != null)
            {
                playerScript.RightClickOff();
            }
        }
    }

    public void OnLeftButtonClick()
    {
         playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Mg19Player playerScript = playerObject.GetComponent<Mg19Player>();
            if (playerScript != null)
            {
                playerScript.LeftClick();
            }
        }
    }

    public void OnLeftButtonClickOff()
    {
         playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Mg19Player playerScript = playerObject.GetComponent<Mg19Player>();
            if (playerScript != null)
            {
                playerScript.LeftClickOff();
            }
        }
    }

    public void FindPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

}
