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
        rend.sprite = bodies[Random.Range(0, bodies.Length)];
    }
}
