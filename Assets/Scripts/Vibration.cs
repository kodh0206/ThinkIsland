using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration: MonoBehaviour
{   
     private static Vibration _instance;
    public static Vibration Instance { get { return _instance; } }
    public bool isVibrate = true;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        LoadState();
    }

    public void Vibrate()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (isVibrate)
        {
            Handheld.Vibrate();
        }
#endif
    }

    public void SaveState()
    {
        PlayerPrefs.SetInt("IsVibrate", isVibrate ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadState()
    {
        if (PlayerPrefs.HasKey("IsVibrate"))
        {
            isVibrate = PlayerPrefs.GetInt("IsVibrate") == 1 ? true : false;
        }
    }

    private void OnApplicationQuit()
    {
        SaveState();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveState();
        }
    }


}
