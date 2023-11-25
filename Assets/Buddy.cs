using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Buddy : MonoBehaviour
{
    public Sprite[] bodies;
    public SpriteRenderer rend;
    public GameObject[] membres;
    Animator anim;
    public GameObject poufSFX;

    private void Start()
    {
        RandomBodies();
        anim = this.GetComponentInChildren<Animator>();
        anim.enabled = false;
        StartCoroutine(startAnim());
        
    }
    public void removeMembres()
    {
        foreach(GameObject go in membres)
        {
            DestroyImmediate(go);
        }
    }
    IEnumerator startAnim()
    {
        yield return new WaitForSeconds(Random.Range(0.1f,2f));
        anim.enabled = true;    
    }

    [ContextMenu("Random Bodies")]
    void RandomBodies()
    {
        //Debug.Log("Randoming Bodying");
        rend.sprite = bodies[Random.Range(0, bodies.Length)];
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            RandomBodies();
        }*/
    }

    public void Kill()
    {
        
        StartCoroutine(Killing());
    }

    IEnumerator Killing()
    {
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(1);
        poufSFX.SetActive(true);
        removeMembres();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
