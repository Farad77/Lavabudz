using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Lava detected smth");
        if(other.gameObject.GetComponentInChildren<Buddy>() != null)
        {
            Debug.Log("Lava detected buddy, Kill Buddy");
            GameManager.Instance.KillBuddy(other.gameObject.GetComponentInChildren<Buddy>());
        }
    }
}
