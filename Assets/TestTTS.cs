using Meta.WitAi.TTS.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTTS : MonoBehaviour
{

    public TTSSpeaker tts;
    // Start is called before the first frame update
    void Start()
    {
        tts.Speak("Bonjour et bienvenue sur le systeme de tts");
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
