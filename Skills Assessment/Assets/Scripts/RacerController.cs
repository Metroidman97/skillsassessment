using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RacerController : MonoBehaviour
{
    // Declare variables
    public Transform[] points;      // Array of waypoints
    private int destPoint = 0;      // Index of next waypoint
    private NavMeshAgent agent ;    // Navmesh agent component

    public int lap = 1;             // Lap counter, starts at 1

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   // Get the navmesh agent component from the object
        agent.autoBraking = false;              // Turn off autobreaking for the navmesh agent
        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (agent.remainingDistance < 5f)
        {
            if (points.Length == 0)
                return;

            agent.destination = points[destPoint].position;

            destPoint = (destPoint + 1) % points.Length;
        }
    }
}
