using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public Transform checkpointsTransform;                  // Checkpoint parent object transform

    [SerializeField] private List<GameObject> racerList;    // List of racers
    private List<Checkpoint> checkpointList;                // List of checkpoints
    private List<int> nextIndexList;                        // List of checkpoint indexes for each racer

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
    }
    
    public void RacerThroughCheckpoint (Checkpoint checkpoint, GameObject racer)
    {
        int nextIndexSingle = nextIndexList[racerList.IndexOf(racer)];

        if (checkpointList.IndexOf(checkpoint) == nextIndexSingle)    // Increment index when checkpoint is passed for each racer
        {
            nextIndexList[racerList.IndexOf(racer)] = (nextIndexSingle + 1) % checkpointList.Count;     // Loop the checkpoint index back to 0 when the racers complete a lap
            Debug.Log(racer.name + " passed " +  checkpoint.name);
        }
    }
}
