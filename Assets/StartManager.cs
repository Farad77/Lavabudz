using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{

    public static StartManager Instance;

    public GameObject[] startElements;
    public GameObject tablePlacer; //use to place virtual table

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        //StartCoroutine(LaunchGameDelay());
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
