﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    public GameObject player;
    public Scene currentScene;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Save()
    {
        //save level
        PlayerPrefs.SetString("currentLevel", currentScene.name);
        PlayerPrefs.SetInt("levelNum", currentScene.buildIndex);
        //save player position
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
        //save player stats
        PlayerPrefs.SetInt("hasJump", (player.GetComponent<PlayerController>().hasJump ? 1 : 0));
        PlayerPrefs.SetInt("hasDash", (player.GetComponent<PlayerController>().hasDash ? 1 : 0));
        PlayerPrefs.SetInt("hasGlide", (player.GetComponent<PlayerController>().hasGlide ? 1 : 0));
        PlayerPrefs.SetInt("extraJumps", player.GetComponent<PlayerController>().extrajumpsMax);
    }

    public void ResetPlayerPrefs()
    {
        //save level
        PlayerPrefs.SetString("currentLevel", null);
        PlayerPrefs.SetInt("levelNum", 0);
        //save player position
        PlayerPrefs.SetFloat("PlayerX", 0);
        PlayerPrefs.SetFloat("PlayerY", 0);
        PlayerPrefs.SetFloat("PlayerZ", 0);
        //save player stats
        PlayerPrefs.SetInt("hasJump", 0);
        PlayerPrefs.SetInt("hasDash", 0);
        PlayerPrefs.SetInt("hasGlide", 0);
        PlayerPrefs.SetInt("extraJumps", 0);
        Debug.Log("game reset");
    }
}
