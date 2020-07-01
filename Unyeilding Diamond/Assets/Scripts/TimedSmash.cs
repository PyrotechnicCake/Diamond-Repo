using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSmash : MonoBehaviour
{
    public float timer;
    public float startDelay;
    private Vector3 target;
    private Vector3 returnPos;
    public int speed;
    private bool smash = false;
    private bool resetting = false;
    public GameObject returnPoint;
    public GameObject targetPoint;
    public GameObject respawn;

    void Start()
    {
        returnPos = returnPoint.transform.position;
        target = targetPoint.transform.position;
        StartCoroutine(StartDelay());
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
            if(transform.position == returnPos)
            {
                resetting = false;
                StartCoroutine(Countdown());
            }
        }

    }
    public IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        yield return new WaitForSeconds(timer);
        smash = true;
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
