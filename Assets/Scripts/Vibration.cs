using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration: MonoBehaviour
{   
    private static Vibration _instance;
    public static Vibration Instance { get { return _instance; } }
    public bool isVibrate;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Vibrate()
    {
        #if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
        #endif
    }


}
