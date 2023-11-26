using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCollant : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "decor"|| collision.gameObject.tag == "Floor")
        {
            FixedJoint joint = this.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody=collision.gameObject.GetComponent<Rigidbody>();
            joint.breakForce = 50;
        }
    }
}
