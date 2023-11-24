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

        if (xDist >= threshold || xDist <= -threshold)
        {
            Move();
        }

        if (zDist >= threshold || zDist <= -threshold)
        {
            Move();
        }
    }

    void Move()
    {
        transform.position = new Vector3(targetHip.transform.position.x, transform.position.y, targetHip.transform.position.z);
    }
}
