using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg1ButtonController : MonoBehaviour
{
    public void OnRightButtonClick()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Mg1Player playerScript = playerObject.GetComponent<Mg1Player>();
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
            Mg1Player playerScript = playerObject.GetComponent<Mg1Player>();
            if (playerScript != null)
            {
                playerScript.RightClickOff();
            }
        }
    }
}
