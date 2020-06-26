using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float crumbleTime;
    public Collider2D myCollider;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().gravityScale;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine("Collapse");
        }
    }

    IEnumerator Collapse()
    {
        //crumble animation
        yield return new WaitForSeconds(crumbleTime);
        myCollider.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = Vector2.down * .001f;
        Debug.Log("falling");

    }
}
