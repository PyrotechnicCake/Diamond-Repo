using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadButton : MonoBehaviour
{
    public PlayerController player;
    public GameObject indie;
    public Scene currentScene;
    public GameManager gm;
    public SoundManager sm;
    public int levelNum;
    public bool saveAndQuit = false;
    public bool saveAndMenu = false;
    public Animator levelChanger;
    public int time = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentScene = SceneManager.GetActiveScene();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        sm = GameObject.FindGameObjectWithTag("GM").GetComponent<SoundManager>();
        levelChanger = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<Animator>();
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(player);
        PlayerPrefs.SetString("currentLevel", currentScene.name);
        PlayerPrefs.SetInt("levelNum", currentScene.buildIndex);
        if (saveAndMenu)
        {
            SceneManager.LoadScene(1);
            sm.returnToMenu = true;
        }else if (saveAndQuit)
        {
            Application.Quit();
        }
    }

    public void LoadGame()
    {
        sm.musicSource.clip = sm.inGameMusicOne;
        sm.musicSource.Play();
        levelChanger.SetTrigger("FadeOut");
        StartCoroutine(ChangeTimeLoad());
        
    }

    public void NewGame()
    {
        levelChanger.SetTrigger("FadeOut");
        StartCoroutine(ChangeTime());
        gm.lastCheckpoint = "StartPos";
        sm.musicSource.clip = sm.inGameMusicOne;
        sm.musicSource.Play();
        
    }

    public void ResetGame()
    {
        SaveSystem.ErasePlayer();
        Debug.Log("Player data erased");
    }

    IEnumerator ChangeTime()
    {
        yield return new WaitForSeconds(time);
        gm.hasJump = false;
        gm.hasDash = false;
        gm.hasGlide = false;
        gm.extraJumps = 0;
        gm.lastCheckpoint = null;

        SceneManager.LoadScene("Tutorial");
    }
    IEnumerator ChangeTimeLoad()
    {
        yield return new WaitForSeconds(time);
        levelNum = PlayerPrefs.GetInt("levelNum");
        //SceneManager.LoadScene(levelNum);
        //Instantiate(indie);
        //player = indie.GetComponent<PlayerController>();

        PlayerData data = SaveSystem.LoadPlayer();

        gm.hasJump = data.jump;
        gm.hasDash = data.dash;
        gm.hasGlide = data.glide;
        gm.extraJumps = data.extraJumps;
        gm.lastCheckpoint = data.checkpointTag;
        //player.myLastCheckpoint = data.checkpoint;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        gm.playerposition = position;
        SceneManager.LoadScene(levelNum);
        //player.gameObject.transform.position = player.myLastCheckpoint.transform.position;
    }
}
