using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnGround : MonoBehaviour
{
    public Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sol")
        {
            this.transform.position = pos;
        }
    }
}
