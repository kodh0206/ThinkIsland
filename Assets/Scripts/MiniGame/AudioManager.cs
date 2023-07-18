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

    private void Awake()
    {
    if (_instance != null && _instance != this)
    {
        Destroy(gameObject);
        return;
    }

        _instance = this;
        audioSource = GetComponent<AudioSource>();
        //StartMainBgm();
        //Debug.Log("메인 배경음 실행");
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

    public void startMiniGame()
    {
        audioSource.PlayOneShot(minigamebegin);
    }
    public void PlayPoop()
    {
        audioSource.PlayOneShot(poop);
    }

    public void PlayCow() 
    {
        audioSource.PlayOneShot(cow);
    }

    public void GoalKeep()
    {
        audioSource.PlayOneShot(goalKeep);
    }
    public void Goal()
    {
        audioSource.PlayOneShot(goal);
    }
    public void Spider()
    {
        audioSource.PlayOneShot(spider);
    }

    public void Rock()
    {
        audioSource.PlayOneShot(rock);
    }
    //MG13 14
    public void Rock2()
    {
        audioSource.PlayOneShot(rock1);
    }

    public void ShellBreak()
    {
        audioSource.PlayOneShot(shellBreak);
    }

    public void ObstacleFly()
    {
        audioSource.PlayOneShot(obstacleFly);
    }

    public void CapsuleBreak()
    {
        audioSource.PlayOneShot(capsulebreak);
    }

    public void BreakPlatform()
    {
        audioSource.PlayOneShot(breakPlatform);
    }

    public void StartMiniGame()
    {
        audioSource.PlayOneShot(minigamebegin);
    }

     public void PlayMiniGameAudio()
    {
        audioSource.PlayOneShot(minigamebegin);
        StartCoroutine(StartMiniGameDelayed());
    }

    public void PlayEggBreak()
    {
        audioSource.PlayOneShot(eggBreak);
    }

    public void PlayBeehive()
    {
        audioSource.PlayOneShot(beeHive);
    }
    private IEnumerator StartMiniGameDelayed()
    {
        yield return new WaitForSeconds(minigamebegin.length);
        MiniGameManager.Instance.StartMiniGame();
    }
}
    