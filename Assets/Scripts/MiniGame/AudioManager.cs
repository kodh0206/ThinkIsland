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

    public AudioClip poop; //똥에 부딪히는 소리
    public AudioClip cow; //소에 부딪히는 소리 

    public AudioClip goalKeep;//골막음
    public AudioClip goal;//골먹힘
    public AudioClip spider; //거미랑 부딪힘

    public AudioClip rock; //돌이랑 부딧힘 
    public AudioClip rock1;//돌이랑 부딧힘2
    public AudioClip shellBreak; //조개껍질 깨지는소리  
    public AudioClip obstacleFly;//장애물날라가기

    public AudioClip capsulebreak;
    public AudioClip breakPlatform;
    public AudioClip minigamebegin;
    public AudioClip eggBreak;
    public AudioClip beeHive;

    public AudioClip miniGameExchange;

    public bool isBGMOn = true; // 기본적으로 BGM 켜짐
    public bool isSFXOn = true; // 기본적으로 SFX 켜짐
    private void Awake()
    {
    if (_instance != null && _instance != this)
    {
        Destroy(gameObject);
        return;
    }
        if (PlayerPrefs.HasKey("BGM"))
            isBGMOn = PlayerPrefs.GetInt("BGM") == 1 ? true : false;

        if (PlayerPrefs.HasKey("SFX"))
            isSFXOn = PlayerPrefs.GetInt("SFX") == 1 ? true : false;
        _instance = this;
        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject); 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

 
    private void OnDestroy() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      if (scene.name == "OpeningCutScene" || MiniGameManager.Instance.isMiniGameScene)
    {
        audioSource.Stop();
    }
    else
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = MainBgm;
            audioSource.Play();
        }
    }
    
    }
      
    private void Start()
    {
    Invoke("StartAudio", 0.5f);
    }

    private void StartAudio()
    {
        if(isBGMOn)
        {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MainBgm;
        audioSource.Play();
        }
    }

    private void StopMainBgm()
    {   if (audioSource.isPlaying)
            audioSource.Stop();
    }

    private void StartMainBgm()
    {     if(isBGMOn)
        {

        audioSource.clip = MainBgm;
        audioSource.Play();
        }
    }

    public void PlayRandomBgm()
    {
        if(isBGMOn)
        {
        int randomIndex = Random.Range(0, bgmClips.Count);
        AudioClip clip = bgmClips[randomIndex];
        audioSource.clip = clip;
        audioSource.Play();

        }
    }

    public void PlayJelly()
    {
        if (isSFXOn)
        {
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(jellySoundEffect);
            audioSource.volume = 1.0f;
        }
    }

    public void startMiniGame()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(minigamebegin);
        }
    }

    public void PlayPoop()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(poop);
        }
    }

    public void PlayCow()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(cow);
        }
    }

    public void GoalKeep()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(goalKeep);
        }
    }

    public void Goal()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(goal);
        }
    }

    public void Spider()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(spider);
        }
    }

    public void Rock()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(rock);
        }
    }

    public void Rock2()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(rock1);
        }
    }

    public void ShellBreak()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(shellBreak);
        }
    }

    public void ObstacleFly()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(obstacleFly);
        }
    }

    public void CapsuleBreak()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(capsulebreak);
        }
    }

    public void BreakPlatform()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(breakPlatform);
        }
    }

    public void StartMiniGame()
    {
        if (isSFXOn)
        {
            audioSource.PlayOneShot(minigamebegin);
        }
    }
     public void PlayMiniGameAudio()
    {
        audioSource.PlayOneShot(minigamebegin);
        StartCoroutine(StartMiniGameDelayed());
    }

    public void PlayEggBreak()
    {   
        if(isSFXOn){
        audioSource.PlayOneShot(eggBreak);
        }
    }

    public void PlayBeehive()
    {   if(isSFXOn){
        audioSource.PlayOneShot(beeHive);
        }
    }

    public void MiniGameExchange()
    {   if(isSFXOn)
        {
        audioSource.PlayOneShot(miniGameExchange);
        }
    }

    private IEnumerator StartMiniGameDelayed()
    {
        yield return new WaitForSeconds(minigamebegin.length);
        MiniGameManager.Instance.StartMiniGame();
    }

    public void StopBGM()
    {
        if(audioSource.isPlaying)
            audioSource.Stop();
    }

    public void PlayBGM()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = MainBgm;
            audioSource.Play();
        }
    }

    public void PlayError()
    {
        
    }
}
    