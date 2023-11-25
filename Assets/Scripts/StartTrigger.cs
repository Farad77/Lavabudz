using Meta.Voice.Samples.WitShapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class StartTrigger : MonoBehaviour
{



    public string triggerTag = "RuneStart";
    public bool isTriggered = false;
    public bool isGameStarted = false;

    public Renderer triggerMaterial;
    UnityEngine.Color baseColor = new UnityEngine.Color(0, 0, 0, 1);
    public UnityEngine.Color activatedColor = new UnityEngine.Color(1, 1, 1, 1);



    private void Awake()
    {
        baseColor = triggerMaterial.material.color;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            DoStartGame();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            DoOsef();
        }
    }



    [ContextMenu("DoStartGame")]
    public void DoStartGame()
    {
        if (!isGameStarted)
        {
            Debug.Log("START GAME");
            // START GAME HERE !!!!!!!!!!!!!!!!
            StartManager.Instance.LaunchGame();
            isGameStarted = true;
        }
        isTriggered = true;
        triggerMaterial.material.color = activatedColor;

    }

    public void DoOsef()
    {
        isTriggered = false;
        triggerMaterial.material.color = baseColor;

    }



}
