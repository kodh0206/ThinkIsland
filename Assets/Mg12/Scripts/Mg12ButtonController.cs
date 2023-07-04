using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg12ButtonController : MonoBehaviour
{
    public void OnRightButtonClick()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Mg12Player playerScript = playerObject.GetComponent<Mg12Player>();
            if (playerScript != null)
            {
                playerScript.RightClick();
            }
        }
    }

    public void OnRightButtonClickoff()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Mg12Player playerScript = playerObject.GetComponent<Mg12Player>();
            if (playerScript != null)
            {
                playerScript.RightClickOff();
            }
        }
    }

    public void OnLeftButtonClick()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Mg12Player playerScript = playerObject.GetComponent<Mg12Player>();
            if (playerScript != null)
            {
                playerScript.LeftClick();
            }
        }
    }

    public void OnLeftButtonClickOff()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Mg12Player playerScript = playerObject.GetComponent<Mg12Player>();
            if (playerScript != null)
            {
                playerScript.LeftClickOff();
            }
        }
    }
}
