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
       if(scene.buildIndex >= 2)
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
