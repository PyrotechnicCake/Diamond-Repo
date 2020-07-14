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
    public int checkPointNum;
    public float[] position;

    public PlayerData (PlayerController player)
    {
        jump = player.hasJump;
        dash = player.hasDash;
        glide = player.hasGlide;
        extraJumps = player.extrajumpsMax;
        checkPointNum = player.checkpointNum;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }

}

