using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.position = respawn.transform.position;
            Debug.Log("Player is kill");
            //reduce lives?
            foreach (GameObject fallingPlatform in fallingPlatforms)
            {
                fallingPlatform.GetComponent<FallingPlatform>().Return();
            }
        }
    }
}
