using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool jump;
    public bool dash;
    public bool glide;
    public int extraJumps;
    public string checkpointTag;
    //public float [] checkpointPos;
    public float[] position;

    public PlayerData (PlayerController player)
    {
        jump = player.hasJump;
        dash = player.hasDash;
        glide = player.hasGlide;
        extraJumps = player.extrajumpsMax;
        checkpointTag = player.myLastCheckpoint.tag;

        /*checkpointPos = new float[3];
        checkpointPos[0] = player.myLastCheckpoint.transform.position.x;
        checkpointPos[1] = player.myLastCheckpoint.transform.position.y;
        checkpointPos[2] = player.myLastCheckpoint.transform.position.z;*/

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }

}

