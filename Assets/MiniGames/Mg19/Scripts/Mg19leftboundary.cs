using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg19leftboundary : MonoBehaviour
{
    public GameObject playerPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MakeOtherSide());
            // 기존 플레이어의 위치를 반전시킴

        }
    }


    IEnumerator MakeOtherSide()
    {
        

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 newPosition = player.transform.position;
        newPosition.x = -newPosition.x - 0.5f;
        


        Destroy(player);

        yield return new WaitForSeconds(0.02f);

        GameObject newPlayer = Instantiate(playerPrefab);
        newPlayer.transform.position = newPosition;

        Player newPlayerComponent = newPlayer.GetComponent<Player>();
        if (newPlayerComponent != null)
        {
            newPlayerComponent.enabled = true;
        }
        Mg19ButtonController.instance.FindPlayer();
    }

}
