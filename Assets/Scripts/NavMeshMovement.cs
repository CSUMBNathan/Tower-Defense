using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform childTransform;
    
    private void Start()
    {
        SetDestination();
        childTransform = transform.GetChild(0);

    }

    void Update()
    {
        childTransform.transform.rotation = Quaternion.Euler(35, 0, 0);
    }
    
    void SetDestination()
    {
        Vector3 endpointPosition = new Vector3(5f, 0f, -70f);
        agent.SetDestination(endpointPosition);
    }
}
