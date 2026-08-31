using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UIController : MonoBehaviour
{
    // Declare variables
    public GameObject nameText;
    public GameObject objectivesText;

    public GameObject raceText;
    public GameObject toRaceText;
    public GameObject backText;

    public Camera mainCamera;
    private RaceManager raceManager;

    private Vector3 cameraStartPosition =  new Vector3(0, 1, -10);
    private Quaternion cameraStartRotation = Quaternion.identity;
    
    private Vector3 cameraRaceposition = new Vector3(-6.43f, -25.7f, 66.6f);
    private Quaternion cameraRaceRotation = Quaternion.Euler(34.7f, 0, 0);

    private enum demoState
    {
        None,
        Race
    }

    private demoState currentState;

    // Start is called before the first frame update
    void Start()
    {
        raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();

        // Set the text to inactive at the start
        nameText.SetActive(false);
        objectivesText.SetActive(false);
        raceText.SetActive(false);
        backText.SetActive(false);

        currentState = demoState.None;

        mainCamera.transform.position = cameraStartPosition;
        mainCamera.transform.rotation = cameraStartRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // If name text is off when P is pressed, turn on. If it's on, turn it off.
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!nameText.activeSelf)
                nameText.SetActive(true);
            else if (nameText.activeSelf)
                nameText.SetActive(false);
        }

        // If the objectives text is off when Q is pressed, turn on. If it's on, turn it off.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!objectivesText.activeSelf)
                objectivesText.SetActive(true);
            else if (objectivesText.activeSelf)
                objectivesText.SetActive(false);
        }

 

        if (currentState == demoState.None)
        { 
            if (Input.GetKeyDown(KeyCode.R))
            {
                mainCamera.transform.position = cameraRaceposition;
                mainCamera.transform.rotation = cameraRaceRotation;
                currentState = demoState.Race;
                raceText.SetActive(true);
                toRaceText.SetActive(false);
                backText.SetActive(true);
            }
        }

        if (currentState == demoState.Race)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                raceManager.StartRace();
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                raceManager.ResetRace();
            }
        }

        if (currentState != demoState.None)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                mainCamera.transform.position = cameraStartPosition;
                mainCamera.transform.rotation = cameraStartRotation;
                currentState = demoState.None;
                raceText.SetActive(false);
                backText.SetActive(false);
                toRaceText.SetActive(true);
                raceManager.ResetRace();
            }
        }
    }
}
