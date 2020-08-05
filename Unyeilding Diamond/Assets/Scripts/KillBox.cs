using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Analytics;

public class KillBox : MonoBehaviour
{
    public Collider2D killBox;
    public GameObject startPos;
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;
    public GameObject checkpoint4;
    public GameObject gm;
    public GameObject[] fallingPlatforms;
    public GameObject[] movingPlatforms;
    public GameObject[] proxSmashers;
    public PlayerController PC;

    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        startPos = GameObject.FindGameObjectWithTag("StartPos");
        checkpoint1 = GameObject.FindGameObjectWithTag("Checkpoint1");
        checkpoint2 = GameObject.FindGameObjectWithTag("Checkpoint2");
        checkpoint3 = GameObject.FindGameObjectWithTag("Checkpoint3");
        checkpoint4 = GameObject.FindGameObjectWithTag("Checkpoint4");
        gm = GameObject.FindGameObjectWithTag("GM");


        //respawn = GameObject.FindGameObjectWithTag("Respawn");
        if (fallingPlatforms.Length == 0)
        {
            fallingPlatforms = GameObject.FindGameObjectsWithTag("FallingPlatform");
        }
        if(movingPlatforms.Length == 0)
        {
            movingPlatforms = GameObject.FindGameObjectsWithTag("MovingPlatform");
        }
        if(proxSmashers.Length == 0)
        {
            proxSmashers = GameObject.FindGameObjectsWithTag("ProximitySmasher");
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(PC.Death == 0)
            {
                PC.Death += 1;
                anim.SetBool("Gliding", false);
                anim.SetBool("Death Finish", false);
                Respawn(col.gameObject);
            }
            
        }
    }

    public void Respawn(GameObject player)
    {
        player.GetComponent<PlayerController>().walkingSounds.Stop();
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        player.GetComponent<PlayerController>().enabled = false;
        if (player.transform.parent != null)
        {
            player.transform.parent = null;
        }
        gm.GetComponent<SoundManager>().PlayerDied();
        StartCoroutine(DeathAnimation(player));






        
        //analytics
        Analytics.CustomEvent("ThePlayierDied", new Dictionary<string, object>
        {
            {"PlayedKilledBy", gameObject.name },
            {"PlayerDeathCount", player.GetComponent<PlayerController>().deathCount}
        });
    }

    IEnumerator DeathAnimation(GameObject player)
    {
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(3);

        //play death Anim instead?
        //add to death count
        player.GetComponent<PlayerController>().deathCount++;
        //find last checkpoint
        player.transform.position = player.GetComponent<PlayerController>().myLastCheckpoint.transform.position;
        player.GetComponent<PlayerController>().enabled = true;
        anim.SetBool("Death Finish", true);
        PC.Death = 0;
        //reset platforms
        foreach (GameObject fallingPlatform in fallingPlatforms)
        {
            fallingPlatform.GetComponent<FallingPlatform>().Return();
        }
        foreach (GameObject movingPlatform in movingPlatforms)
        {
            movingPlatform.GetComponent<MovingPlatform>().Return();
        }
        foreach (GameObject proxSmasher in proxSmashers)
        {
            proxSmasher.GetComponent<ProximitySmash>().Return();
        }
    }
}
