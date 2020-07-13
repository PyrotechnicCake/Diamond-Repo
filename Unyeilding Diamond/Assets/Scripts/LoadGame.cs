using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    public Scene currentLevel;
    private int levelNum;
    public GameObject player;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel.name = PlayerPrefs.GetString("currentLevel");
        levelNum = PlayerPrefs.GetInt("levelNum");
        if(PlayerPrefs.GetInt("hasJump") == 0)
        {
            button.GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        Debug.Log(levelNum);
        SceneManager.LoadScene(levelNum);
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(
            PlayerPrefs.GetFloat("PlayerX"),
            PlayerPrefs.GetFloat("PlayerY"),
            PlayerPrefs.GetFloat("PlayerZ"));
        player.GetComponent<PlayerController>().hasJump = PlayerPrefs.GetInt("hasJump") != 0;
        player.GetComponent<PlayerController>().hasDash = PlayerPrefs.GetInt("hasDash") != 0;
        player.GetComponent<PlayerController>().hasGlide = PlayerPrefs.GetInt("hasGlide") != 0;
        player.GetComponent<PlayerController>().extrajumpsMax = PlayerPrefs.GetInt("extraJumps");

    }
}
