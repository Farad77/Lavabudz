using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Buddy : MonoBehaviour
{
    public Sprite[] bodies;
    public SpriteRenderer rend;
    Animator anim;

    private void Start()
    {
        RandomBodies();
        anim = this.GetComponentInChildren<Animator>();
        anim.enabled = false;
        StartCoroutine(startAnim());
        
    }
    IEnumerator startAnim()
    {
        yield return new WaitForSeconds(Random.Range(0.1f,2f));
        anim.enabled = true;    
    }

    [ContextMenu("Random Bodies")]
    void RandomBodies()
    {
        rend.sprite = bodies[Random.Range(0, bodies.Length)];
    }
}
