using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //load player stuff
    public bool hasJump;
    public bool hasDash;
    public bool hasGlide;
    public int extraJumps;
    public string lastCheckpoint;
    public Vector3 playerposition;
    public GameObject player;
    public PlayerController pc;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        Debug.Log("startfunction called");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
