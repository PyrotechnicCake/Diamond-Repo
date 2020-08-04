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
    public AudioSource source;
    public AudioClip fall;
    public AudioClip crush;
    private float lowPitch = .75f;
    private float highPitch = 1.5f;

    void Start()
    {
        returnPos = returnPoint.transform.position;
        target = targetPoint.transform.position;
        StartCoroutine(StartDelay());
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (smash)
        {
            source.clip = fall;
            source.Play();
            gameObject.GetComponentInChildren<KillBox>().killBox.enabled = true;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position == target)
            {
                source.Stop();
                source.pitch = Random.Range(lowPitch, highPitch);
                source.PlayOneShot(crush);
                smash = false;
                resetting = true;
            }
        }
        if (resetting)
        {
            gameObject.GetComponentInChildren<KillBox>().killBox.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, returnPos, (speed / 2) * Time.deltaTime);
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
