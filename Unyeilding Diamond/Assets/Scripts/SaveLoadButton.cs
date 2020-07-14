using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadButton : MonoBehaviour
{
    public PlayerController player;
    public Scene currentScene;
    public int levelNum;
    public bool saveAndQuit = false;
    public bool saveAndMenu = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentScene = SceneManager.GetActiveScene();
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
        SceneManager.LoadScene(levelNum);

        PlayerData data = SaveSystem.LoadPlayer();

        player.hasJump = data.jump;
        player.hasDash = data.dash;
        player.hasGlide = data.glide;
        player.extrajumpsMax = data.extraJumps;
        player.checkpointNum = data.checkPointNum;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.gameObject.transform.position = position;
    }

    public void ResetGame()
    {
        SaveSystem.ErasePlayer();
    }
}
