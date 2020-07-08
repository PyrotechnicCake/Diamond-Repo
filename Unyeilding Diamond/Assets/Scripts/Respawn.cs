using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Respawn : MonoBehaviour
{
    public int myCheckPointNum;
    public CinemachineVirtualCamera cam;
    public int resetCamPriorityTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().checkpointNum = myCheckPointNum;
            cam.Priority = resetCamPriorityTo;
        }
    }
}
