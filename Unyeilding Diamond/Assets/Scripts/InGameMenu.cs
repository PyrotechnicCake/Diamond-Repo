using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject sAndQ;
    public GameObject options;
    public GameObject menu;
    public GameObject player;
    public Scene currentScene;
    private bool menuOpen;

    // Start is called before the first frame update
    void Start()
    {
        sAndQ.SetActive(false);
        options.SetActive(false);
        menu.SetActive(false);
        menuOpen = false;
        player = GameObject.FindGameObjectWithTag("Player");
        currentScene = SceneManager.GetActiveScene();
    }

    public void Clicked()
    {
        if (menuOpen)
        {
            HideMenu();
        }
        else if (!menuOpen)
        {
            RevealMenu();
            Debug.Log("Show Menu");
        }
    }

    public void RevealMenu()
    {
        sAndQ.SetActive(true);
        options.SetActive(true);
        menu.SetActive(true);
        menuOpen = true;
    }
    public void HideMenu()
    {
        sAndQ.SetActive(false);
        options.SetActive(false);
        menu.SetActive(false);
        menuOpen = false;
    }

    public void Menu()
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

        SceneManager.LoadScene(0);
    }
}
