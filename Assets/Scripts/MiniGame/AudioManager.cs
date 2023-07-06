using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{   
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }
    public List<AudioClip> bgmClips;
    public  AudioSource audioSource;
    public AudioClip jellySoundEffect; // 젤리 효과음
    public AudioClip MainBgm; //메인 배경음

    private void Awake()
    {
    if (_instance != null && _instance != this)
    {
        Destroy(gameObject);
        return;
    }

        _instance = this;
        audioSource = GetComponent<AudioSource>();
        StartMainBgm();
        Debug.Log("메인 배경음 실행");
        DontDestroyOnLoad(gameObject); 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

 
    private void OnDestroy() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      if (scene.name != "Opening" && !MiniGameManager.Instance.isMiniGameScene)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.clip = MainBgm;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    
    }
      
    private void Start()
    {
    Invoke("StartAudio", 0.5f);
    }

    private void StartAudio()
    {
    audioSource = GetComponent<AudioSource>();
    audioSource.clip = MainBgm;
    audioSource.Play();
    }

    private void StopMainBgm()
    {
        audioSource.Stop();
    }

    private void StartMainBgm()
    {
        audioSource.clip = MainBgm;
        audioSource.Play();
    }

    public void PlayRandomBgm()
    {
    int randomIndex = Random.Range(0, bgmClips.Count);
    AudioClip clip = bgmClips[randomIndex];
    audioSource.clip = clip;
    audioSource.Play();
    }

    public void PlayJelly()
    {   
        audioSource.volume =0.5f;
        audioSource.PlayOneShot(jellySoundEffect);
        audioSource.volume =1.0f;
    }
}