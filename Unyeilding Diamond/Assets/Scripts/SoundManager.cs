using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip titleMusic;
    public AudioClip inGameMusicOne;
    public bool returnToMenu;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        source = GetComponent<AudioSource>();
        source.PlayOneShot(titleMusic);
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
       if(scene.buildIndex == 2)
       {
            source.Stop();
            source.PlayOneShot(inGameMusicOne);
       }
        if (returnToMenu)
        {
            source.Stop();
            source.PlayOneShot(titleMusic);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
