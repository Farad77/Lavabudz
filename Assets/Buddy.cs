using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buddy : MonoBehaviour
{
    public Sprite[] bodies;
    public SpriteRenderer rend;

    private void Start()
    {
        RandomBodies();
    }

    [ContextMenu("Random Bodies")]
    void RandomBodies()
    {
        Debug.Log("Randoming Bodying");
        rend.sprite = bodies[Random.Range(0, bodies.Length)];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RandomBodies();
        }
    }
}
