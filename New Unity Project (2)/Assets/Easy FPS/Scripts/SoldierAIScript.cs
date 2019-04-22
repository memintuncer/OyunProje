using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SoldierAIScript : MonoBehaviour
{
    SoldierBehaviour bhvr;
    NavMeshAgent mNavMeshAgent;
    Actions act;
    // Start is called before the first frame update
    void Start()
    {
        bhvr = this.GetComponent<SoldierBehaviour>();
        mNavMeshAgent = this.GetComponent<NavMeshAgent>();
        act = this.GetComponent<Actions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bhvr.enemyFound)
        {
            // Check if we've reached the destination
            if (!mNavMeshAgent.pathPending &&
                mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance &&
                (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude == 0f))
            {
                act.Stay();
            }

            else
            {
                act.Walk();
                mNavMeshAgent.SetDestination(bhvr.lastKnownPosition);
            }
        }
        
    }
}
