using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private RaceManager raceManager;    // Race manager script object

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Racer")
        {
            raceManager.RacerThroughCheckpoint(this, other.gameObject);       // Signal to the race manager when a racer goes through a checkpoint
        }
    }

    public void SetRaceManager (RaceManager raceManager)
    {
        this.raceManager = raceManager;     // Get the race manager script
    }
}
