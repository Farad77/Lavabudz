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
        if (target != null&&Vector3.Distance(target.position,this.transform.position)<=3)
        {
            nav.SetDestination(target.position);
        }
        else
        {
            nav.isStopped = true;
        }

    }
}
