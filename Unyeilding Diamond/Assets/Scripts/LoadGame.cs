using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public Scene currentLevel;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel.name = PlayerPrefs.GetString("currentLevel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Load()
    {
        SceneManager.LoadScene(currentLevel.name);
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
