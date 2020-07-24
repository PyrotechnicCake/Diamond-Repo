using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource ambienceSource;
    public AudioSource SFXSource;

    public AudioClip titleMusic;
    public AudioClip inGameMusicOne;
    public AudioClip jungleAmb;
    public AudioClip ruinsAmb;
    public AudioClip waterfallAmb;
    public AudioClip waterfallFoley;
    public AudioClip deathClip;
    private AudioClip resumeMusic;

    public bool returnToMenu;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        musicSource.clip = titleMusic;
        musicSource.Play();
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
       if(scene.buildIndex >= 2 && scene.buildIndex != 11)
       {
            musicSource.Stop();
            musicSource.clip = inGameMusicOne;
            musicSource.Play();
            ambienceSource.clip = jungleAmb;
            ambienceSource.Play();
       }
        if (returnToMenu)
        {
            musicSource.Stop();
            musicSource.clip = titleMusic;
            musicSource.Play();
        }
        if(scene.buildIndex == 11)
        {
            musicSource.Stop();
            musicSource.clip = titleMusic;
            musicSource.Play();
        }
    }

    public void PlayerDied()
    {
        resumeMusic = musicSource.clip;
        musicSource.Stop();
        musicSource.PlayOneShot(deathClip);
        ResumeMusic();
    }
    void ResumeMusic()
    {
        musicSource.clip = resumeMusic;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
