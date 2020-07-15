using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasures : MonoBehaviour
{  
    public enum TreasureTypes
    {
        Jump,
        AddJump,
        Dash,
        Glide,
        Fly,

    }

    public TreasureTypes myType;
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        switch (myType)
        {
            case TreasureTypes.Jump:
                break;
            case TreasureTypes.AddJump:
                break;
            case TreasureTypes.Dash:
                break;
            case TreasureTypes.Glide:
                break;
            case TreasureTypes.Fly:
                break;
        }
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && myType == TreasureTypes.Jump)
        {
            collision.GetComponent<PlayerController>().hasJump = true;
            gm.hasJump = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player" && myType == TreasureTypes.AddJump)
        {
            collision.GetComponent<PlayerController>().extrajumpsMax++;
            gm.extraJumps ++;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player" && myType == TreasureTypes.Dash)
        {
            collision.GetComponent<PlayerController>().hasDash = true;
            gm.hasDash = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player" && myType == TreasureTypes.Glide)
        {
            collision.GetComponent<PlayerController>().hasGlide = true;
            gm.hasGlide = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player" && myType == TreasureTypes.Fly)
        {
            collision.GetComponent<PlayerController>().hasFly = true;
            Destroy(gameObject);
        }
    }
}
