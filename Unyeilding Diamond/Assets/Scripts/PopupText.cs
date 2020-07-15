using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    public int PopTime;
    
    public GameObject Pop;
   
    SpriteRenderer P;
    
    // Start is called before the first frame update
    void Start()
    {
        P = Pop.GetComponent<SpriteRenderer>();
        P.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            P.enabled = true;
            
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(PopTime);
        Destroy(Pop);
    }
}
