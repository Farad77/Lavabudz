using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererFollower : MonoBehaviour
{
    public Transform pos1, pos2;
    LineRenderer lineRend;
    public float lineWidth = 0.01f;


    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.startWidth = lineWidth;
        lineRend.endWidth = lineWidth;
    }

   
    void Update()
    {
        lineRend.SetPosition(0, pos1.position);
        lineRend.SetPosition(1, pos2.position);
    }
}
