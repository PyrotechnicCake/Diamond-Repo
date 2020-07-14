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
    public int levelNum;
    public bool saveAndQuit = false;
    public bool saveAndMenu = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentScene = SceneManager.GetActiveScene();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(player);
        PlayerPrefs.SetString("currentLevel", currentScene.name);
        PlayerPrefs.SetInt("levelNum", currentScene.buildIndex);
        if (saveAndMenu)
        {
            SceneManager.LoadScene(0);
        }else if (saveAndQuit)
        {
            Application.Quit();
        }
    }

    public void LoadGame()
    {
        
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

    public void ResetGame()
    {
        SaveSystem.ErasePlayer();
        Debug.Log("Player data erased");
    }
}
