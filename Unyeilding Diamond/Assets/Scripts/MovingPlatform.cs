using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public GameObject destination;
    public Vector2 target;
    private Vector2 pos;
    private Vector2 tempPos;
    private float distance;
    public bool isActive = false;
    private bool reached = false;
    
    // Start is called before the first frame update
    void Start()
    {
        target = destination.transform.position;
        pos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //Debug.Log("moving");
            Move();
        }

        if (!reached)
        {
            distance = Vector2.Distance(transform.position, target);
            if(distance < .1)
            {
                //Debug.Log("destination reached");
                reached = true;
            }
        }
        else if(reached)
        {
            //Debug.Log("changing destintion");
            tempPos = target;
            target = pos;
            pos = tempPos;
            reached = false;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log("activated");
            //start moving
            isActive = true;
        }
    }
    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
