using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RacerController : MonoBehaviour
{
    // Declare variables
    public Transform[] points;              // Array of waypoints
    private int destPoint = 0;              // Index of next waypoint
    private NavMeshAgent agent ;            // Navmesh agent component

    public int lap = 1;                     // Lap counter, starts at 1

    public bool isRacing;                   // Determines if the racer is currently racing
    private bool raceDone;                  // Determines if the racer has finished the race

    private Vector3 startPosition;          // The starting position of the racer
    private Quaternion startRotation;       // The starting rotation of the racer

    public TextMeshProUGUI positionText;    // UI text for race position

    public int checkPointCount;            // Counts how many checkpoints the racer went through
    public int racerPosition;

    public GameObject[] finishText;

    public TextMeshProUGUI lapText;

    private void Awake()
    {
        // Set the racer's starting positions and rotations
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();       // Get the navmesh agent component from the object
        agent.autoBraking = false;                  // Turn off autobreaking for the navmesh agent
        isRacing = false;                           // Racer is currently not racing until the race officially starts
        raceDone = false;                           // Racer has not finished the race
        positionText.text = gameObject.name + ": "; // Set the position text to blank
        lap = 1;
        lapText.text = "Lap: " + lap + "/3";
        checkPointCount = 0;

        for (int i = 0; i < finishText.Length; i++)
        {
            finishText[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Only move to next point when the race is going
        if (isRacing)
        {
            GoToNextPoint();
            UpdatePositionText();
        }
    }

    void GoToNextPoint()
    {
        // Get the racer moving if they aren't
        if (agent.isStopped)
        {
            agent.isStopped = false;
        }

        // Change the racer's waypoint when they get close to their current target one
        if (agent.remainingDistance < 5f)
        {
            if (points.Length == 0)
                return;

            agent.destination = points[destPoint].position;

            destPoint = (destPoint + 1) % points.Length;    // Increment the waypoint counter, looping when the max is reached
        }
    }

    public void IncrementLap(int maxLaps)
    {
        lap++;      // Increment the lap count when a lap is finished
        if (lap > maxLaps && !raceDone)
        {
            raceDone = true;    // Finish the race for that racer when the cross the finish line on lap 3
            finishText[racerPosition - 1].SetActive(true);
        }
    }

    public void ResetRacer()
    {
        // Reset bools
        isRacing = false;
        raceDone = false;
        
        // Briefly disable the navmesh agent so as to not get caught by the navmesh when resetting the position
        agent.enabled = false;
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = startRotation;
        agent.enabled = true;

        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        // Reset the various counters
        lap = 1;
        destPoint = 0;
        agent.destination = points[destPoint].position;
        checkPointCount = 0;
        for (int i = 0; i < finishText.Length; i++)
        {
            finishText[i].SetActive(false);
        }
    }

    public void UpdatePositionText()
    {
        positionText.text = gameObject.name + ": " + racerPosition + "/4";
        lapText.text = "Lap: " + lap + "/3";
    }

    public void IncrementCheckPoints()
    {
        checkPointCount++;
    }
}
