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
        //add to death count
        player.GetComponent<PlayerController>().deathCount++;
        
        //reset player
        player.transform.position = respawn.transform.position;
        //reduce lives?
        
        //reset platforms
        foreach (GameObject fallingPlatform in fallingPlatforms)
        {
            fallingPlatform.GetComponent<FallingPlatform>().Return();
        }
        //analytics
        Analytics.CustomEvent("ThePlayierDied", new Dictionary<string, object>
        {
            {"PlayedKilledBy", gameObject.name },
            {"PlayerDeathCount", player.GetComponent<PlayerController>().deathCount}
        });
    }
}
