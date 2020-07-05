using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class KillBox : MonoBehaviour
{
    public Collider2D killBox;
    public GameObject respawn;
    public GameObject[] fallingPlatforms;
    
    // Start is called before the first frame update
    void Start()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        if(fallingPlatforms.Length == 0)
        {
            fallingPlatforms = GameObject.FindGameObjectsWithTag("FallingPlatform");
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Respawn(col.gameObject);
           
        }
    }

    public void Respawn(GameObject player)
    {
        //analytics
        Dictionary<string, object> customParams = new Dictionary<string, object>();
        customParams.Add("WhatKilledPlayer", gameObject.name);
        Debug.Log(gameObject.name);
        //reset player
        player.transform.position = respawn.transform.position;
        //reduce lives?
        //add to death count
        player.GetComponent<PlayerController>().deathCount++;
        //reset platforms
        Debug.Log("deathcount = " + player.GetComponent<PlayerController>().deathCount);
        foreach (GameObject fallingPlatform in fallingPlatforms)
        {
            fallingPlatform.GetComponent<FallingPlatform>().Return();
        }
    }
}
