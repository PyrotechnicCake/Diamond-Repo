using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChange : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public int priorityNum;
    public CinemachineVirtualCamera cam2;
    public int priorityNum2;
    public CinemachineVirtualCamera cam3;
    public int priorityNum3;


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
            cam.Priority = priorityNum;
            cam2.Priority = priorityNum2;
            cam3.Priority = priorityNum3;
        }
    }
}
