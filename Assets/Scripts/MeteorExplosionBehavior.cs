using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorExplosionBehavior : MonoBehaviour
{



    // ON COLLISION ON THE FLOOR
    // this cript will manage the VFX of the Explosion/Impact
    // Decals, Explosion FXs and Triggers for the budz death


    [SerializeField] Transform explosionTrigger;
    [SerializeField] float explosionRadius;
    [SerializeField] LayerMask killableBuddz;

    [SerializeField] GameObject explosionFX;
    [SerializeField] GameObject impactDecal;

    [SerializeField] GameObject aoeDecalBG;
    [SerializeField] GameObject aoeDecal;


    [SerializeField] Animator meteorAnim;
    [SerializeField] Rigidbody meteorRb;




    // Start is called before the first frame update
    // normalement les gamesObjects en ref devraient etre déja initialisés
    void Start()
    {
        LaunchMeteor();

    }

    // Update is called once per frame
    void Update()
    {
        //UpdateAoeDecal(0);
    }





    [ContextMenu("LaunchMeteor")]
    public void LaunchMeteor()
    {
        InitMeteor();
    }

    void InitMeteor()
    {
        //explosionTrigger.SetActive(false);
        explosionFX.SetActive(false);
        impactDecal.SetActive(false);

        aoeDecalBG.SetActive(true);
        aoeDecal.SetActive(true);
        aoeDecal.transform.localScale = new Vector3(0, 0, 0);

    }






    void UpdateAoeDecal(float distWithImpactPoint)
    {
        // le aoeDecalBG devrai etre systematiquemnt sous le meteorite
        // le aoeDecal soit etre annimé (0 --> 1) en fonction de sa distance avec le future point d'impacte
    }








    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("On collision meteor "+collision.gameObject.name);
        if (collision.collider.CompareTag("Floor")|| collision.collider.CompareTag("Buddy"))
        {
            DoExplode();
        }
        Destroy(gameObject);
    }


     Collider[] collidedBudds;
    [ContextMenu("DoExplode")]
    public void DoExplode()
    {
        // lancer le fx d'explosion
        // activer le detecteur de budz
        // kill les budz dans la zone
        Debug.Log("explosion");
        Debug.Log("Meteor Exploding : " + gameObject.name);
        //explosionTrigger.SetActive(true);

        explosionFX.SetActive(false);
        explosionFX.SetActive(true);
        GameObject impact = Instantiate(impactDecal, null);
        impact.SetActive(true);

        aoeDecalBG.SetActive(false);
        aoeDecal.SetActive(false);
        aoeDecal.transform.localScale = new Vector3(3, 3, 3);

        collidedBudds = Physics.OverlapSphere(explosionTrigger.position, explosionRadius, killableBuddz);
        foreach(Collider c in collidedBudds){

            GameManager.Instance.KillBuddy(c.gameObject.GetComponent<Buddy>());   

        }
        
    }



}
