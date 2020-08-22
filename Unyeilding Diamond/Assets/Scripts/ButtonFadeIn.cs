using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFadeIn : MonoBehaviour
{
    public Animator anim;
    public int t;
    
    
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(ButtonFade());
    }

    IEnumerator ButtonFade()
    {
        yield return new WaitForSeconds(t);
        anim.SetTrigger("Play");
        anim.SetTrigger("Menu");
        anim.SetTrigger("End");
    }

}
