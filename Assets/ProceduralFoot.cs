using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralFoot : MonoBehaviour
{
    public GameObject targetHip;

    public float xDist, zDist;
    public float threshold = 1; //au bout de 1m, move

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        xDist = targetHip.transform.position.x - transform.position.x;
        zDist = targetHip.transform.position.z - transform.position.z;

        if (xDist >= threshold)
        {
            Move(1f,0f);
        }
        else if (xDist <= -threshold)
        {
            Move(-1f,0f);
        }

        if (zDist >= threshold)
        {
            Move(0f,1f);
        }
        else if (zDist <= -threshold)
        {
            Move(0f,-1f);
        }
    }

 

    void Move(float x, float z)
    {
        transform.position = new Vector3(targetHip.transform.position.x + x/17, transform.position.y, targetHip.transform.position.z + z / 17);
    }
}
