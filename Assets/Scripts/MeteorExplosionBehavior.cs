using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
//using System.Numerics;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorExplosionBehavior : MonoBehaviour
{

    [SerializeField] bool noKillExplosion = false;


    [SerializeField] Vector3 baseParentPosition;
    [SerializeField] float rndParentPositionOffset = 3;

    [SerializeField] float baseHeight = 30;
    [SerializeField] float rndHeightOffset = 10;


    // ON COLLISION ON THE FLOOR
    // this cript will manage the VFX of the Explosion/Impact
    // Decals, Explosion FXs and Triggers for the budz death
    [SerializeField] float safeTicLoopDelay = 10;
    [SerializeField] float delayBeforKill = 1;

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
        //baseHeight = transform.localPosition.y;
        baseParentPosition = transform.parent.localPosition;
        //LaunchMeteor();
        //InitMeteor();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateAoeDecal(0);
    }



    Coroutine safeTicLoop;

    [ContextMenu("LaunchMeteor")]
    public void LaunchMeteor()
    {
        if (safeTicLoop != null)
            StopCoroutine(safeTicLoop);
        safeTicLoop = StartCoroutine(SafeTicLoop());

        InitMeteor();
        //StartCoroutine(InitMeteor());

    }


    /// <summary>
    /// Initialise ou réinitialise ce météore
    /// </summary>
    void InitMeteor()
    {


        Debug.Log("INIT METEOR");

        //explosionTrigger.SetActive(false);
        explosionFX.SetActive(false);
        impactDecal.SetActive(false); // already deactivate

        // prepa pour l'affichage du feedback
        aoeDecalBG.SetActive(true);
        aoeDecal.SetActive(true);
        aoeDecal.transform.localScale = new Vector3(0, 0, 1);




        // ----------------------------------------------------------
        // ca marche pas bien ici
        // le meteor veux pas se reinitialiser en haut !
        // ----------------------------------------------------------


        meteorRb.velocity = Vector3.zero;
        meteorRb.angularVelocity = Vector3.zero;
        //float parentPositionX = Random.Range(-rndParentPositionOffset, rndParentPositionOffset) + baseParentPosition.x;
        //float parentPositionZ = Random.Range(-rndParentPositionOffset, rndParentPositionOffset) + baseParentPosition.z;
        //transform.parent.localPosition = new Vector3(parentPositionX, baseParentPosition.y, parentPositionZ);
        transform.rotation = Quaternion.identity;
        //float newHeight = baseHeight + Random.Range(-rndHeightOffset, rndHeightOffset);

        //yield return new WaitForEndOfFrame();

        //transform.localPosition = new Vector3(0, newHeight, 0);
        Vector3 newPos = new Vector3(0, baseHeight, 0);
        transform.localPosition = newPos;


        //meteorRb.isKinematic = false;
        distMax = CalculateDistance();
        updateAoeDecal = StartCoroutine(UpdateAoeDecal());
    }

    /// <summary>
    /// détruit l'object au bout du temps impartis
    /// </summary>
    IEnumerator SafeTicLoop()
    {
        // waiting for safety Death
        yield return new WaitForSeconds(safeTicLoopDelay);

        //Destroy(transform.parent.gameObject);
        DeactivateMeteor(0);
    }



    /// <summary>
    /// Désactive ce météor et le prépare pour la prochaine utilisation
    /// </summary>
    Coroutine DeactivateMeteor(float t)
    {
        return StartCoroutine(DeactivateMeteor_routine(t));
    }
    IEnumerator DeactivateMeteor_routine(float t)
    {
        yield return new WaitForSeconds(t);

        //StartCoroutine(InitMeteor());
        InitMeteor();
        transform.parent.gameObject.SetActive(false);
        //meteorRb.isKinematic = true;
        //yield return new WaitForSeconds(t);

        StopAllCoroutines();

        Debug.Log("DEACTIVATE METEOR");
    }


    Coroutine updateAoeDecal;
    [SerializeField] LayerMask contactableLayers;
    [SerializeField] float distMax = 20;

    /// <summary>
    /// Gestion du feedback au sol via decal projection
    /// </summary>
    IEnumerator UpdateAoeDecal()
    {
        // le aoeDecal doit etre annimé (0 --> 1) en fonction de sa distance avec le future point d'impacte

        // RAYCAST en -y
        // prochain floor/sol/budz touché determine la distance
        // distance calculé en tps reel pour rapport dist/distMax -> normalisation
        // valeur à multiplier par la taille de l'AOE scale(3,3,1)

        // en gros 3xdist/distMax


        // pourait etre fait en update seulement xD fatigué
        while (true)
        {
            float dist = CalculateDistance();
            //float normalizedDist = distMax / (3 * dist);
            float normalizedDist = 3 * (1 - dist / distMax);

            aoeDecal.transform.localScale = new Vector3(normalizedDist, normalizedDist, 1);

            yield return new WaitForEndOfFrame();
        }

    }


    float CalculateDistance()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        Ray ray = new Ray(origin, direction);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100, contactableLayers))
            return hitInfo.distance;
        else
            return 100;
    }






    /// <summary>
    /// explose au contact
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor") || collision.collider.CompareTag("Buddy") || collision.collider.CompareTag("Sol"))
        {
            DoExplode();
        }
        else
            Debug.Log("Meteor collided but do NOT Explode on : " + collision.gameObject.name);

        //Destroy(transform.parent.gameObject, delayBeforKill);
        DeactivateMeteor(delayBeforKill);
    }




    Collider[] collidedBudds;
    [ContextMenu("DoExplode")]
    public void DoExplode()
    {
        if (updateAoeDecal != null)
            StopCoroutine(updateAoeDecal);
        // lancer le fx d'explosion
        // activer le detecteur de budz
        // kill les budz dans la zone
        Debug.Log("Meteor Exploding on : " + gameObject.name);
        //explosionTrigger.SetActive(true);

        explosionFX.SetActive(false);
        explosionFX.SetActive(true);

        GameObject impact = Instantiate(impactDecal, impactDecal.transform.position, impactDecal.transform.rotation, null);
        impact.SetActive(true);


        aoeDecalBG.SetActive(false);
        aoeDecal.SetActive(false);
        aoeDecal.transform.localScale = new Vector3(3, 3, 1);

        if (!noKillExplosion)
        {
            collidedBudds = Physics.OverlapSphere(explosionTrigger.position, explosionRadius, killableBuddz);
            foreach (Collider c in collidedBudds)
            {
                GameManager.Instance.KillBuddy(c.gameObject.GetComponent<Buddy>());
            }
        }

    }





}
