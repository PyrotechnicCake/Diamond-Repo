using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySmash : MonoBehaviour
{
    private Vector3 target;
    private Vector3 returnPos;
    public int speed;
    public bool smash = false;
    public bool resetting = false;
    public GameObject returnPoint;
    public GameObject targetPoint;
    public GameObject respawn;

    void Start()
    {
        returnPos = returnPoint.transform.position;
        target = targetPoint.transform.position;
    }

    void Update()
    {
        if (smash)
        {
            gameObject.GetComponentInChildren<KillBox>().killBox.enabled = true;
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position == target)
            {

                smash = false;
                resetting = true;
            }
        }
        if (resetting)
        {
            gameObject.GetComponentInChildren<KillBox>().killBox.enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, returnPos, (speed / 2) * Time.deltaTime);
            if (transform.position == returnPos)
            {
                resetting = false;
            }
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log(col.name);
            col.transform.position = respawn.transform.position;
        }
    }
}
