using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingRock : MonoBehaviour
{
    public Vector3 ogPos;
    public float resetSpeed;
    private Rigidbody2D rb;
    public bool returning;
    //public bool sinking;
    public float resurfaceTime;
    public float shakeTime;
    //private bool shakeStopped;
    //private bool sunk;
    public Animator anim;




    // Start is called before the first frame update
    void Start()
    {
        ogPos = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (returning)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, ogPos, resetSpeed * Time.deltaTime);
            Debug.Log("Return");
        }
        if(gameObject.transform.position == ogPos)
        {
            returning = false;
        }
        /*if (sinking && shakeStopped)
        {
            Sink();
        }*/
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //shake?
            Debug.Log("On Rock");
            StartCoroutine(Shake());
            //sink

        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && sunk)
        {
            //return
            Debug.Log("Off Rock");
            returning = true;
            sinking = false;
            sunk = false;
        }
    }*/

    IEnumerator Resuface()
    {
        yield return new WaitForSeconds(resurfaceTime);
        returning = true;
        //shakeStopped = false;
        //sunk = false;
        //sinking = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    

    IEnumerator Shake()
    {
        anim.SetTrigger("Sinking");
        yield return new WaitForSeconds(shakeTime);
        //sinking = true;
        //shakeStopped = true;
        Sink();
    }

    public void Sink()
    {
        
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        //rb.velocity = Vector2.down * .001f;
        Debug.Log("sink");
        //sunk = true;
        StartCoroutine(Resuface());
        
    }

    
    public void Return()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = ogPos;
    }
}
