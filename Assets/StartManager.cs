using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    public GameObject[] startElements;
    public GameObject tablePlacer; //use to place virtual table

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LaunchGameDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LaunchGameDelay()
    {

        yield return new WaitForSeconds(10f);
        LaunchGame();
    }


    [ContextMenu("Launch Game")]
    public void LaunchGame() //by pressing start Button
    {
        HideStartElements();
        GameManager.Instance.Init(tablePlacer.transform.position);
    }

    private void HideStartElements()
    {
        foreach (GameObject element in startElements)
        {
            element.SetActive(false);
        }
    }
}
