using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class KillBox : MonoBehaviour
{
    public Collider2D killBox;
    public GameObject startPos;
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;
    private int checkRespawn;

    public GameObject[] fallingPlatforms;
    public GameObject[] movingPlatforms;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = GameObject.FindGameObjectWithTag("StartPos");
        checkpoint1 = GameObject.FindGameObjectWithTag("Checkpoint1");
        checkpoint2 = GameObject.FindGameObjectWithTag("Checkpoint2");
        checkpoint3 = GameObject.FindGameObjectWithTag("Checkpoint3");

        //respawn = GameObject.FindGameObjectWithTag("Respawn");
        if (fallingPlatforms.Length == 0)
        {
            fallingPlatforms = GameObject.FindGameObjectsWithTag("FallingPlatform");
        }
        if(movingPlatforms.Length == 0)
        {
            movingPlatforms = GameObject.FindGameObjectsWithTag("MovingPlatform");
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Respawn(col.gameObject, checkRespawn);
           
        }
    }

    public void Respawn(GameObject player, int checkpointNum)
    {
        //add to death count
        player.GetComponent<PlayerController>().deathCount++;
        //find last checkpoint
        player.transform.position = player.GetComponent<PlayerController>().myLastCheckpoint.transform.position;

       
        //reset platforms
        foreach (GameObject fallingPlatform in fallingPlatforms)
        {
            fallingPlatform.GetComponent<FallingPlatform>().Return();
        }
        foreach (GameObject movingPlatform in movingPlatforms)
        {
            movingPlatform.GetComponent<MovingPlatform>().Return();
        }
        //analytics
        Analytics.CustomEvent("ThePlayierDied", new Dictionary<string, object>
        {
            {"PlayedKilledBy", gameObject.name },
            {"PlayerDeathCount", player.GetComponent<PlayerController>().deathCount}
        });
    }
}
