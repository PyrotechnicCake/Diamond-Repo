using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScrollSingle : MonoBehaviour
{

    //public float ScrollX1 = -0.5f;
    //public float ScrollY1 = -0.5f;
    public float ScrollX2 = 0.5f;
    public float ScrollY2 = 0.5f;
    //public float ScrollX3 = 0.5f;
    //public float ScrollY3 = 0.5f;
    public Material[] M;
    void Start()
    {
        M = GetComponent<Renderer>().materials;
    }

    void Update()
    {
        //float offsetX1 = Time.time * ScrollX1;
        //float offsetY1 = Time.time * ScrollY1;
        float offsetX2 = Time.time * ScrollX2;
        float offsetY2 = Time.time * ScrollY2;
        //float offsetX3 = Time.time * ScrollX3;
        //float offsetY3 = Time.time * ScrollY3;
        //M[1].mainTextureOffset = new Vector2(offsetX1, offsetY1);
        M[0].mainTextureOffset = new Vector2(offsetX2, offsetY2);
        //M[3].mainTextureOffset = new Vector2(offsetX3, offsetY3);
    }
}