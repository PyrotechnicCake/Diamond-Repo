using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingRock : MonoBehaviour
{
    public Vector3 ogPos;
    public float resetSpeed;
    private Rigidbody2D rb;
    public bool returning;
    public GameObject colToIgnore;



    // Start is called before the first frame update
    void Start()
    {
        ogPos = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Physics2D.IgnoreCollision(colToIgnore.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>(),true);
    }
    private void Update()
    {
        if (returning)
        {
            MoveBack();
        }
        if(gameObject.transform.position == ogPos)
        {
            returning = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //shake?
            //sink
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            rb.velocity = Vector2.down * .001f;
            Debug.Log("sink");
        }
        if(collision.collider.isTrigger == true)
        {
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //return
            returning = true;
            
        }
    }

    public void MoveBack()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = Vector3.MoveTowards(gameObject.transform.position, ogPos, resetSpeed * Time.deltaTime);
        Debug.Log("Return");
    }
    
    public void Return()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = ogPos;

    }
}
