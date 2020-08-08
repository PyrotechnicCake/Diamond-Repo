using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource ambienceSource;
    public AudioSource SFXSource;
    //music
    public AudioClip titleMusic;
    public AudioClip inGameMusicOne;
    //ambience
    public AudioClip jungleAmb;
    public AudioClip statuesAmb;
    public AudioClip synthRuins;
    public AudioClip sythnOne;
    public AudioClip synthTwo;
    //sfx
    public AudioClip deathClip;
    private AudioClip resumeMusic;
    public AudioClip waterfallH;
    public AudioClip waterfallL;
    public AudioClip treasureCollect;

    public GameObject[] waterfalls;


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
       if(scene.buildIndex >= 2 && scene.buildIndex <= 4)
       {
            //jump levels
            //musicSource.clip = inGameMusicOne;
            //musicSource.Play();
            ambienceSource.clip = jungleAmb;
            ambienceSource.Play();
       }
        if (scene.buildIndex == 5 || scene.buildIndex == 6)
        {
            //dash levels
            //musicSource.clip = inGameMusicOne;
            //musicSource.Play();
            ambienceSource.Stop();
            ambienceSource.clip = statuesAmb;
            ambienceSource.Play();
        }
        if (scene.buildIndex == 7)
        {
            //double jump levels
            //musicSource.clip = inGameMusicOne;
            //musicSource.Play();
            ambienceSource.Stop();
            ambienceSource.clip = waterfallL;
            ambienceSource.Play();
        }
        if (scene.buildIndex == 8)
        {
            //double jump levels
            //musicSource.clip = inGameMusicOne;
            //musicSource.Play();
            ambienceSource.Stop();
            ambienceSource.volume = .15f;
            ambienceSource.clip = waterfallH;
            ambienceSource.Play();
        }
        if (scene.buildIndex == 9 || scene.buildIndex == 10)
        {
            //glide levels
            //musicSource.clip = inGameMusicOne;
            //musicSource.Play();
            ambienceSource.Stop();
            ambienceSource.volume = .1f;
            ambienceSource.clip = synthRuins;
            ambienceSource.Play();
        }
        if (returnToMenu)
        {
            ambienceSource.Stop();
            musicSource.Stop();
            musicSource.clip = titleMusic;
            musicSource.Play();
            returnToMenu = false;
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
