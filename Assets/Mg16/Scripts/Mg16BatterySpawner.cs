using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mg16BatterySpawner : MonoBehaviour
{
    Mg16Battery mg16Battery;
    public GameObject battery;

    public Transform player;


    //[SerializeField]
    //private float batterySpeed = 5.0f;

    [SerializeField]
    public float time_diff = 4f;

    float time = 0;

    void Awake()
    {
        
    }
    private void Start() 
    {
        mg16Battery = GetComponent<Mg16Battery>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > time_diff)
        {
            GameObject new_battery = Instantiate(battery);

            Vector2 spawnPosition = new Vector2(player.position.x , player.position.y);
            new_battery.transform.position = spawnPosition;

            new_battery.GetComponent<Mg16Battery>();
            time = 0;
            
            if (new_battery != null)
            {
                Destroy(new_battery, 1.0f);
            }
        }
    }
}
