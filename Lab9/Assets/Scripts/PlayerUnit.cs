using UnityEngine;
using System.Collections;

public class PlayerUnit : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Start ()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void SetDestination(Vector3 location)
    {
        navMeshAgent.SetDestination(location);
    }
}
