using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheChildrenAreBelongToDie : MonoBehaviour
{

    public GameObject[] enfants;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void generationDeenfanter()
    {
        foreach(GameObject enfant in enfants)
        {
            enfant.SetActive(true);
            enfant.transform.parent = null;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
