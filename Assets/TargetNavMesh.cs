using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetNavMesh : MonoBehaviour
{

    public Transform target;

    UnityEngine.AI.NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        nav = this.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        if (target != null&&Vector3.Distance(target.position,this.transform.position)<=6)
        {
            nav.SetDestination(target.position);
        }
        if (target != null && Vector3.Distance(target.position, this.transform.position) > 8)
            //|| Vector3.Distance(target.position, this.transform.position) <=2)
        {
            nav.isStopped = true;
        }
        /* else
         {
             nav.isStopped = true;
         }*/

    }
}
