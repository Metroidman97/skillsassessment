using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public Transform checkpointsTransform;                  // Checkpoint parent object transform

    [SerializeField] private List<GameObject> racerList;    // List of racers
    private List<Checkpoint> checkpointList;                // List of checkpoints
    private List<int> nextIndexList;                        // List of checkpoint indexes for each racer

    public int maxLaps = 3;                                 // Maximum number of laps

    private bool isRaceGoing;                               // Bool for controlling the state of the race

    private void Awake()
    {
        //Transform checkpointsTransform = transform.Find("Checkpoints"); This doesn't work for some reason

        checkpointList = new List<Checkpoint>(); // Initialize checkpoint list

        foreach (Transform checkpointSingle in checkpointsTransform)
        {
            // Get the checkpoint script component from each checkpoint object and give them the racemanager script object
            Checkpoint checkpoint = checkpointSingle.GetComponent<Checkpoint>();    
            checkpoint.SetRaceManager(this);

            checkpointList.Add(checkpoint);     // Add the checkpoint to the list
        }

        nextIndexList = new List<int>();        // Initialize index list
        foreach (GameObject racer in racerList)
        {
            nextIndexList.Add(0);   // Add an index for each racer
        }

        isRaceGoing = false;    // Initialize the race state to false
    }

    private void Start()
    {
        //SetPositions();
    }

    private void Update()
    {
        //TrackPositions();
    }

    public void RacerThroughCheckpoint (Checkpoint checkpoint, GameObject racer)
    {
        int nextIndexSingle = nextIndexList[racerList.IndexOf(racer)];

        if (checkpointList.IndexOf(checkpoint) == nextIndexSingle)    // Increment index when checkpoint is passed for each racer
        {
            if (nextIndexSingle == (checkpointList.Count - 1))
            {
                racer.GetComponent<RacerController>().IncrementLap(maxLaps);
            }
            nextIndexList[racerList.IndexOf(racer)] = (nextIndexSingle + 1) % checkpointList.Count;     // Loop the checkpoint index back to 0 when the racers complete a lap
            racer.GetComponent<RacerController>().IncrementCheckPoints();       // Increment the checkpoint counter
        }

        ComparePositions(racerList.IndexOf(racer));
    }

    void ComparePositions(int racerNumber)
    {
        if (racerList[racerNumber].GetComponent<RacerController>().racerPosition > 1)
        {
            GameObject currentRacer = racerList[racerNumber];
            int currentRacerPosition = currentRacer.GetComponent<RacerController>().racerPosition;
            int currentRacerCheckPoints = currentRacer.GetComponent<RacerController>().checkPointCount;

            GameObject racerFrontRunner = null;
            int racerFrontRunnerPosition = 0;
            int racerFrontRunnerCheckPoints = 0;

            for (int i = 0; i < racerList.Count; i++)
            {
                if (racerList[i].GetComponent<RacerController>().racerPosition == currentRacerPosition - 1)
                {
                    racerFrontRunner = racerList[i];
                    racerFrontRunnerCheckPoints = racerFrontRunner.GetComponent<RacerController>().checkPointCount;
                    racerFrontRunnerPosition = racerFrontRunner.GetComponent<RacerController>().racerPosition;
                    break;
                }
            }

            if (currentRacerCheckPoints > racerFrontRunnerCheckPoints)
            {
                currentRacer.GetComponent<RacerController>().racerPosition = currentRacerPosition - 1;
                racerFrontRunner.GetComponent<RacerController>().racerPosition = racerFrontRunnerPosition + 1;
            }
        }
    }

    public void StartRace()
    {
        // Start the race if it isn't going already
        SetPositions();
        if (!isRaceGoing)
        {
            foreach (GameObject racer in racerList)
            {
                racer.GetComponent<RacerController>().isRacing = true;
            }
        }
        isRaceGoing = true;
    }

    public void ResetRace()
    {
        // Reset the race if it's currently going
        if (isRaceGoing)
        {
            isRaceGoing = false;
            foreach (GameObject racer in racerList)
            {
                racer.GetComponent<RacerController>().ResetRacer();
            }
        }
    }

    void SetPositions()
    {
        for (int i = 0; i < racerList.Count; i++)
        {
            racerList[i].GetComponent<RacerController>().racerPosition = i + 1;
            //racerList[i].GetComponent<RacerController>().racerNumber = i;
        }
    }
}
