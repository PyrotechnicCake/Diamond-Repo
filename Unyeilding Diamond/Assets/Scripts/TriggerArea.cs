using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public ProximitySmash smasher;
    // Start is called before the first frame update
    void Start()
    {
        smasher = GetComponentInParent<ProximitySmash>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !smasher.resetting)
        {
            smasher.smash = true;
        }
    }
}
