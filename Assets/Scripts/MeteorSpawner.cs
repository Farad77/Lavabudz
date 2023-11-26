using System.Collections;
using Unity.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    [SerializeField, ReadOnly] bool infiniteLoop = true;
    public bool EnableInfiniteLoop
    {
        set => infiniteLoop = true;
    }
    public bool DisableInfiniteLoop
    {
        set => infiniteLoop = false;
    }
    public bool GetIsInfiniteLoop
    {
        get => infiniteLoop;
    }

    [SerializeField] bool isSpawning = false;

    public float delay = 1;
    public float rndDelay = 0.5f;

    public GameObject[] meteorsAspawn;






    public void StartSpawning()
    {
        Debug.Log("START TO SPAWN METEORS");
        isSpawning = true;
        StartCoroutine(SpawnLoop(/*delay, rndDelay*/));
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }


    IEnumerator SpawnLoop(/*float delay, float rndDelay*/)
    {
        while (isSpawning)
        {
            foreach (GameObject meteor in meteorsAspawn)
            {
                float delayToAdd = Random.Range(-rndDelay, rndDelay);
                yield return new WaitForSeconds(delay + delayToAdd);

                Debug.Log("SPAWNING A METEOR");
                meteor.SetActive(true);
                MeteorExplosionBehavior meteorVisual = meteor.GetComponentInChildren<MeteorExplosionBehavior>();
                meteorVisual.LaunchMeteor();
            }
        }
    }




}
